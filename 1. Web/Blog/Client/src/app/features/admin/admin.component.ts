import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { distinctUntilChanged, filter, map, Observable, startWith } from 'rxjs';
import { IBreadcrumb } from 'src/app/shared/interfaces/breadcrumb.type';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
})
export class AdminComponent {
  breadcrumbs$: Observable<IBreadcrumb[]> | undefined;
  contentHeaderDisplay: string | undefined;
  isFolded: boolean | undefined;
  isSideNavDark: boolean | undefined;
  isExpand: boolean | undefined;
  selectedHeaderColor: string | undefined;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private titleService: Title
  ) {
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        map(() => {
          let child = this.activatedRoute.firstChild;
          while (child) {
            if (child.firstChild) {
              child = child.firstChild;
            } else if (
              child.snapshot.data &&
              child.snapshot.data['headerDisplay']
            ) {
              return child.snapshot.data['headerDisplay'];
            } else {
              return null;
            }
          }
          return null;
        })
      )
      .subscribe((data: any) => {
        this.contentHeaderDisplay = data;
      });
  }

  ngOnInit() {
    this.titleService.setTitle('Admin Dashboard');
    this.breadcrumbs$ = this.router.events.pipe(
      startWith(new NavigationEnd(0, '/', '/')),
      filter((event) => event instanceof NavigationEnd),
      distinctUntilChanged(),
      map((data) => this.buildBreadCrumb(this.activatedRoute.root))
    );
  }

  private buildBreadCrumb(
    route: ActivatedRoute,
    url: string = '',
    breadcrumbs: IBreadcrumb[] = []
  ): IBreadcrumb[] {
    let label = '',
      path = '/',
      display = null;

    if (route.routeConfig) {
      if (route.routeConfig.data) {
        label = route.routeConfig.data['title'];
        path += route.routeConfig.path;
      }
    } else {
      label = 'Dashboard';
      path += 'dashboard';
    }

    const nextUrl = path && path !== '/dashboard' ? `${url}${path}` : url;
    const breadcrumb = <IBreadcrumb>{
      label: label,
      url: nextUrl,
    };

    const newBreadcrumbs = label
      ? [...breadcrumbs, breadcrumb]
      : [...breadcrumbs];
    if (route.firstChild) {
      return this.buildBreadCrumb(route.firstChild, nextUrl, newBreadcrumbs);
    }
    return newBreadcrumbs;
  }
}
