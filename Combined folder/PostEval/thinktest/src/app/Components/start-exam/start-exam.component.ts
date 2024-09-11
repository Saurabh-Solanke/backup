import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-start-exam',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FormsModule, HttpClientModule,],
  templateUrl: './start-exam.component.html',
  styleUrls: ['./start-exam.component.css']
})
export class StartExamComponent {
  termsAccepted: boolean = false;
  constructor(private router: Router) {
    sessionStorage.setItem('subjectId', '1');
    sessionStorage.setItem('subjectName', 'Angular'); }  
  startExam() {
    if (!this.termsAccepted) {
      Swal.fire({
        icon: 'error',
        title: 'Terms not accepted',
        text: 'Please accept the terms and conditions to start the exam',
      });
      return;
    } else {
      Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to start the exam?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, start!',
        cancelButtonText: 'No, cancel'
      }).then((result) => {
        if (result.isConfirmed) {          
          this.router.navigate(['/exam']);
        }
      });
    }
  }

}
