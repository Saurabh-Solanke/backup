import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import Swal from 'sweetalert2';
import { StartExamComponent } from './Components/start-exam/start-exam.component';
import { ExamComponent } from './Components/exam/exam.component';
import { ResultComponent } from './Components/result/result.component';
import { CandidateHomeComponent } from './Components/candidate-home/candidate-home.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,StartExamComponent,ExamComponent,ResultComponent,CandidateHomeComponent,PageNotFoundComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'online-mcq-exam';
  
}
