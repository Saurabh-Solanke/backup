import { Component } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-test-instruction',
  templateUrl: './test-instruction.component.html',
  styleUrl: './test-instruction.component.css'
})
export class TestInstructionComponent {
  termsAccepted: boolean = false;
  constructor(private router: Router) {
   }
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
          this.router.navigate(['/user/exam']);
        }
      });
    }
  }
}
