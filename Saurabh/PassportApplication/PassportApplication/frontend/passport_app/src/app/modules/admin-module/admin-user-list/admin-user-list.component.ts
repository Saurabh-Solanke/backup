import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AdminNavbarComponent } from "../admin-navbar/admin-navbar.component";

@Component({
  selector: 'app-users',
  templateUrl: './admin-user-list.component.html',
  styleUrls: ['./admin-user-list.component.css'], // Optional, for any styling you may want to add
  standalone: true,
  imports: [CommonModule, AdminNavbarComponent]
})
export class AdminUserListComponent {
  users: any[] = [];
  loading: boolean = false;
  apiUrl = 'http://localhost:5172/api/Users'; // Replace with your actual API URL

  constructor(private http: HttpClient) {
    this.fetchUsers();
  }

  fetchUsers() {
    this.loading = true;
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.users = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching users:', err);
        this.loading = false;
      }
    });
  }

  deleteUser(userId: number) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.http.delete(`${this.apiUrl}/${userId}`).subscribe({
        next: () => {
          this.users = this.users.filter(user => user.userId !== userId);
        },
        error: (err) => {
          console.error('Error deleting user:', err);
        }
      });
    }
  }
}
