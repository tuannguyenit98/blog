import { Inject, Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserLogin } from '../models/user-login.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  public currentUserSubject: BehaviorSubject<UserLogin>;
    public logoutSubject$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public loginSubject$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public currentUser: Observable<UserLogin>;
    public accessToken: string = '';

    public get currentUserValue(): UserLogin {
        return this.currentUserSubject.value;
    }

    currentUserFromLocal(): UserLogin {
        return JSON.parse(localStorage.getItem('currentUser') || '{}');
    }

    constructor(
        http: HttpClient,
        @Inject('API_BASE_URL') baseUrl: string,
        private router: Router
    ) {
        super(http, baseUrl);
        this.currentUserSubject = new BehaviorSubject<UserLogin>(
            JSON.parse(localStorage.getItem('currentUser') || '{}'),
        );
        this.currentUser = this.currentUserSubject.asObservable();
    }

    login(data: any) {
        let fullUrl = 'api/blog/login';

        return this.post<any>(fullUrl, data).pipe(
            map((userLogin: any) => {
                this.setUser(userLogin);
                this.accessToken = userLogin.accessToken;
                this.loginSubject$.next(true);
                return userLogin;
            }),
        );
    }

    logout() {
      localStorage.removeItem('currentUser');
              this.currentUserSubject.next(new UserLogin());
              this.router.navigateByUrl('/auth/login');
              this.accessToken = '';
  }

  logut() {
      let fullUrl = 'api/blog/me/logout';

      return this.post(fullUrl).pipe(map(res => {
          this.logoutSubject$.next(true);
          this.logout();
      }));
  }

  register(data: any) {
      let fullUrl = 'api/blog/register';

      return this.post(fullUrl, data);
  }

  private setUser(userLogin: any): void {
      const user = new UserLogin();
      user.userName = userLogin.userName;
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user);
  }

  updateCurrentUserStorage(key: string, value: string) {
      let localStorageJsonData = JSON.parse(
          localStorage.getItem('currentUser')!,
      );
      switch (key) {
          case 'userDisplayName':
              localStorageJsonData.userDisplayName = value;
              break;
      }
      localStorage.setItem(
          'currentUser',
          JSON.stringify(localStorageJsonData),
      );
  }

  refreshToken() {
      let fullUrl = 'api/blog/refresh-token';

      return this.post(fullUrl);
  }
}
