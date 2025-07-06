import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
    const router = inject(Router);
  const currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}');
  const isLoggedIn = currentUser && currentUser.userName;
  if (isLoggedIn) {
    return true;
  }
  // Chuyển hướng về trang login
  router.navigate(['/auth/login']);
  return false;
};
