import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  user: any;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.getUserInfo();
  }

  getUserInfo(): void {
    const userId = sessionStorage.getItem('userId');  // Fetching userId from sessionStorage
    if (userId) {
      this.apiService.getUserById(userId).subscribe({
        next: (data) => {
          this.user = data;
        },
        error: (err) => {
          console.error('Error fetching user info:', err);
        }
      });
    } else {
      console.warn('No user ID found in session storage.');
    }
  }

  editProfile(): void {
    // Redirect to profile editing page or open a modal
    console.log('Edit profile clicked.');
  }
}