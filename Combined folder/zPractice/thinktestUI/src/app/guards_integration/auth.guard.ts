import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if (typeof sessionStorage !== 'undefined') {
    const loggedUser = sessionStorage.getItem('loggedInUser');
    if (loggedUser !== null) {
      return true;
    } else {
      router.navigate(['/login']);
      return false;
    }
  } else {
    router.navigate(['/login']);
    return false;
  }
};
