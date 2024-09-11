
import { Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-candidate-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './candidate-home.component.html',
  styleUrl: './candidate-home.component.css'
})
export class CandidateHomeComponent {

  name: string | null;

  constructor(private router: Router) {
    this.name = sessionStorage.getItem("name");
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



