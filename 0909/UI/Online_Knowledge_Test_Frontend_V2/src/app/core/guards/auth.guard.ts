import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if (typeof sessionStorage !== 'undefined') {
    const token = sessionStorage.getItem('token');
    if (token !== null) {
      return true;
    } else {
      // router.navigate(['/login']);
      // return false;
      return true;

    }
  } else {
    // router.navigate(['/login']);
    // return false;
    return true;

  }
};
