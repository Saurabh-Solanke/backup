import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { RefreshService } from '../../../services/refresh.service';
import { ToastService } from '../../../services/toast.service';
import { User } from '../../../models/user.model';
import { UserService } from '../../../services/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-profile',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule,CommonModule],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent implements OnInit{

  private router = inject(Router);
  private authService = inject(AuthService);
  private userService = inject(UserService);
  private refreshService = inject(RefreshService);
  private toastService = inject(ToastService);
  
  currentUser: User | null = null;
  updateProfileForm = new FormGroup({
    firstname: new FormControl('',[Validators.minLength(3),Validators.required]),
    lastname: new FormControl('',[Validators.minLength(3),Validators.required]),
    mobileNo: new FormControl('',[Validators.minLength(8),Validators.required])
  });

  ngOnInit(): void {
    this.loadActiveUser();
    if (this.currentUser) {
      this.updateProfileForm.patchValue({
        firstname: this.currentUser.firstname,
        lastname: this.currentUser.lastname,
        mobileNo: this.currentUser.mobileNo
      });
    }
  }

  loadActiveUser() {
    const currentUserJson = localStorage.getItem('currentUser');
    if (currentUserJson) {
      this.currentUser = JSON.parse(currentUserJson);
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  onSubmit() {
    if (this.updateProfileForm.valid && this.currentUser) {
      const updateProfileFormValue = this.updateProfileForm.value;

      // Create updateUser object only with the fields to be updated
      const updateUser: User = {
        userId: this.currentUser.userId,
        firstname: updateProfileFormValue.firstname ?? this.currentUser.firstname,
        lastname: updateProfileFormValue.lastname ?? this.currentUser.lastname,
        email: this.currentUser.email, // Email is not being updated
        password: '', // Assuming password is not part of the update
        mobileNo: updateProfileFormValue.mobileNo ?? this.currentUser.mobileNo,
        passportUserId: this.currentUser.passportUserId, // Assuming this is not part of the update
        role: this.currentUser.role // Retaining the existing role
      };

      this.userService.updateUser(this.currentUser.userId, updateUser).subscribe({
        next: () => {
          // Update current user in local storage
          this.currentUser = updateUser;
          localStorage.setItem('currentUser', JSON.stringify(this.currentUser));
          this.toastService.showSuccess("Profile Updated Successfully");
          this.router.navigate(['user']);
        },
        error: (err) => console.log(err)
      });
    }
  }
}
