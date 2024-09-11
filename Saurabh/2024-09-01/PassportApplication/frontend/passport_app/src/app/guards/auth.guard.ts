import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { ToastService } from '../services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router,private toastService:ToastService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {

    // Check if the user is logged in by using a method from AuthService
    const isLoggedIn = this.authService.isLoggedIn;

    if (isLoggedIn) {
      // If logged in, allow access to the route
      return true;
    } else {
      // If not logged in, redirect to the login page
      this.toastService.showInfo("Please Login First")
      this.router.navigate(['/login']);
      return false;
    }
  }
}
