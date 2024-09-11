import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if (sessionStorage.getItem("loggedInUser") != null) {
    return true;
  } else {
    Swal.fire({
      title: 'Please login first to continue',
      icon: 'warning',
      confirmButtonText: 'OK'
    }).then(() => {
      router.navigate(['/login']);
    });
    return false;
  }
};
