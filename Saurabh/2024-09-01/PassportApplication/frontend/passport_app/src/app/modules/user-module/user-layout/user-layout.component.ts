
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserNavbarComponent } from '../user-navbar/user-navbar.component';
import { BreadcrumbComponent } from '../../shared/breadcrumb/breadcrumb.component';


@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrls: ['./user-layout.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    UserNavbarComponent,
    BreadcrumbComponent,
  ],
})
export class UserLayoutComponent {}
