import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../models/user.model';
import { RefreshService } from '../../services/refresh.service';
import { ToastService } from '../../services/toast.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-user-info',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-info.component.html',
  styleUrl: './user-info.component.css'
})
export class UserInfoComponent implements OnInit {

  private router = inject(Router);
  private authService=inject(AuthService);
  private refreshService = inject(RefreshService);
  private toastService=inject(ToastService)
  currentUser: User | null = null;

  ngOnInit(): void {
      this.loadActiveUsers();
  }

  loadActiveUsers() {
    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
    } else {
      this.currentUser = null;
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']); 
  }
}
