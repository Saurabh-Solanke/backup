import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if (typeof sessionStorage !== 'undefined') {
    const loggedUser = sessionStorage.getItem('loggedUserEmail');
    if (loggedUser !== null) {
      return true;
    } else {
      router.navigate(['']);
      return false;
    }
  } else {
    router.navigate(['']);
    return false;
  }
};
