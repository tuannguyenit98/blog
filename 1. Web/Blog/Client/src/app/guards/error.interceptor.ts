import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor(
    private authService: AuthService, 
    private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}');
    const isLoggedIn = (currentUser && currentUser.userName) ? true : false;
    if (isLoggedIn === false) {
      return next.handle(request)
      .pipe(catchError(err => {
        if (err.status === 401) {
          this.router.navigate(['/']);
          return throwError(err.error);
        }

        return throwError(err.error);
    }));
    }
    if ((this.authService.accessToken != '' && !this.tokenExpired(this.authService.accessToken))) {
      request = this.addToken(request, this.authService.accessToken);
    }

    return next.handle(request).pipe(catchError(error => {
      if ((error instanceof HttpErrorResponse && error.status === 401) || (this.authService.accessToken != '' && this.tokenExpired(this.authService.accessToken)) || this.authService.accessToken == '') {
        return this.handle401Error(request, next);
      }
      else if(error instanceof HttpErrorResponse && error.status === 403) {
        this.router.navigateByUrl('');
        return throwError(error.error);
      }
      else {
        return throwError(error.error);
      }
    }));
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(
        switchMap((token: any) => {
          this.isRefreshing = false;
          if (token.length <= 0) {
            this.isRefreshing = false;
            this.authService.logout();
            this.authService.accessToken = '';
            return throwError('logout');
          }
          this.authService.accessToken = token;
          this.refreshTokenSubject.next(token);
          return next.handle(this.addToken(request, token));
        }));

    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(jwt => {
          return next.handle(this.addToken(request, jwt));
        }));
    }
  }

  tokenExpired(token: string) {
    const expiry = (JSON.parse(atob(token?.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }
}
