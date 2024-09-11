import { Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { LoggedInFooterComponent } from "../../shared/components/logged-in-footer/logged-in-footer.component";
import { TestHistoryComponent } from '../test-history/test-history.component';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, RouterModule, LoggedInFooterComponent, TestHistoryComponent, RouterLink],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent{

  name: string | null;

  // this is my object in session storage : {
    // "email": "sandesh@gmail.com",
    // "phoneNo": null,
    // "userFullname": "Sandesh Tribhuvan",
    // "userId": 1,
    // "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsic2FuZGVzaEBnbWFpbC5jb20iLCJVc2VyIl0sImp0aSI6Ijg3ZDMwNTViLTg5NTUtNDEyNy05OGU4LWM4NmVmYTU3MDdiZiIsImV4cCI6MTcyNTU3MDUxNywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjQ3IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDIwMCJ9.-ztJcu5nTxobqEFKOz2mIxT5PApAb9hSdbY4tsF82dQ",
    // "expiration": "2024-09-06T03:38:37.7582728+05:30",
    // "role": "User"
//}

  constructor(private router: Router) {
    this.name = sessionStorage.getItem("name"); 
    sessionStorage.removeItem('subjectId');
  }


  onLogoutClick() {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to logout?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, logout!',
      cancelButtonText: 'No, stay'
    }).then((result) => {
      if (result.isConfirmed) {

        sessionStorage.clear();

        this.router.navigate(['/']);
      }
    });
  }
}



