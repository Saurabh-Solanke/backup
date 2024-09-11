



import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BreadcrumbComponent } from '../../shared/breadcrumb/breadcrumb.component';
import { UserNavbarComponent } from '../user-navbar/user-navbar.component';
import { FooterComponent } from '../../shared/footer/footer.component';


@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    UserNavbarComponent,
    BreadcrumbComponent,
    FooterComponent,
  ],
})
export class UserHomeComponent implements OnInit {
  username: string = '';
  profileName: string = '';
  profileEmail: string = '';
  profilePhone: string = '';
  recentActivities: string[] = []; 
  normalPlans = [
    {
      title: 'Fresh Passport',
      description: 'Page Count 60 Fee ₹ 2000',
      link: '/new_passport',
      applicationType: 'normal',
      bookletType: '60_pages',
    },
    {
      title: 'Fresh Passport',
      description: 'Page Count 36 Fee ₹ 1500',
      link: '/new_passport',
      applicationType: 'normal',
      bookletType: '36_pages',
    },
    {
      title: 'Reissue Passport',
      description: 'Page Count 60 Fee ₹ 2000',
      link: '/renewal_passport',
      applicationType: 'normal',
      bookletType: '60_pages',
    },
    {
      title: 'Reissue Passport',
      description: 'Page Count 36 Fee ₹ 1500',
      link: '/renewal_passport',
      applicationType: 'normal',
      bookletType: '36_pages',
    },
  ];

  tatkalPlans = [
    {
      title: 'Fresh Passport',
      description: 'Page Count 60 Fee ₹ 4000',
      link: '/new_passport',
      applicationType: 'tatkal',
      bookletType: '60_pages',
    },
    {
      title: 'Fresh Passport',
      description: 'Page Count 36 Fee ₹ 3500',
      link: '/new_passport',
      applicationType: 'tatkal',
      bookletType: '36_pages',
    },
    {
      title: 'Reissue Passport',
      description: 'Page Count 60 Fee ₹ 3500',
      link: '/renewal_passport',
      applicationType: 'tatkal',
      bookletType: '60_pages',
    },
    {
      title: 'Reissue Passport',
      description: 'Page Count 36 Fee ₹ 4000',
      link: '/renewal_passport',
      applicationType: 'tatkal',
      bookletType: '36_pages',
    },
  ];

  ngOnInit(): void {
    this.loadUserData();
  }

  loadUserData(): void {
    const loggedInUser = sessionStorage.getItem('loggedInUser');
    if (loggedInUser) {
      try {
        const user = JSON.parse(loggedInUser);
        this.username = user.name;
        this.profileName = user.name;
        this.profileEmail = user.email;
        this.profilePhone = user.phone;
      } catch (error) {
        console.error('Error parsing user data from session storage', error);
      }
    } else {
      console.warn('No user data found in session storage');
    }
  }
}
