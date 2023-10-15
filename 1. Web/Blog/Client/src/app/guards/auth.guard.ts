import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}');
    const isLoggedIn = currentUser && currentUser.userName;
      if (isLoggedIn) {
          // logged in so return true
          return true;
      }

      // not logged in so redirect to login page with the return url
      state.url = '/blog/login';
      return false;
};
