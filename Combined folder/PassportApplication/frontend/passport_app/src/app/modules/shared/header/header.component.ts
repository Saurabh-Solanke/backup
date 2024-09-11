import { ChangeDetectorRef, Component, DoCheck, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../../models/user.model';
import { UserService } from '../../../services/user.service';
import { RefreshService } from '../../../services/refresh.service';
import { ToastService } from '../../../services/toast.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements  DoCheck {
 
  isActivate: boolean = false;
  private router = inject(Router);
  private authService=inject(AuthService);
  private toastService=inject(ToastService)
  currentUser: User | null = null;

  ngOnInit() {
  }
  ngDoCheck(): void {
    this.checkUserStatus();
  }

  checkUserStatus() {
    const loggedInUser = this.authService.getLoggedInUser();
    if(loggedInUser){

      this.isActivate = true;
    }else{
      this.isActivate=false;
    }
    }

    logout(): void {
      this.authService.logout();
      this.isActivate = false;
      this.router.navigate(['/login']); 
    }
}
