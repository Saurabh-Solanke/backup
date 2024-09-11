import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { SignupComponent } from './Components/signup/signup.component';
import { StartExamComponent } from './Components/start-exam/start-exam.component';
import { authGuard } from './guard/auth.guard';
import { ExamComponent } from './Components/exam/exam.component';
import { ResultComponent } from './Components/result/result.component';


export const routes: Routes = [
  {
    path: '',component: LoginComponent
  },
  {
    path: 'signup', component: SignupComponent
  },
  {
    path: 'start-exam' , component : StartExamComponent, canActivate: [authGuard]
  },
  {
    path: 'exam',component :ExamComponent, canActivate: [authGuard]
  },
  {
    path: 'result',component : ResultComponent, canActivate: [authGuard]
  }
];
