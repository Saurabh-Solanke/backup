import { Component, OnInit } from '@angular/core';
import { UserDetailsInUserManager } from '../../../core/interfaces/user';
import { ApiService } from '../../../core/services/api.service';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-users-dashboard',
  templateUrl: './users-dashboard.component.html',
  styleUrl: './users-dashboard.component.css'
})
export class UsersDashboardComponent implements OnInit {
  users: UserDetailsInUserManager[] = [];
  loading: boolean = true;
  searchValue: string | undefined;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(): void {
    this.apiService.getAllUsers().subscribe((data: UserDetailsInUserManager[]) => {
      this.users = data;
      this.loading = false;
    });
  }

  // Fix the error by casting event.target to HTMLInputElement
  filterGlobal(event: Event, table: Table) {
    const inputElement = event.target as HTMLInputElement;
    table.filterGlobal(inputElement.value, 'contains');
  }

  clear(table: Table) {
    table.clear();
    this.searchValue = '';
  }
}
