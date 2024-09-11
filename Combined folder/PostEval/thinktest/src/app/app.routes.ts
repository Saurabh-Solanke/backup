import { CandidateHomeComponent } from './Components/candidate-home/candidate-home.component';
import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { authGuard } from './service/guard/auth.guard';
import { TestHistoryComponent } from './Components/test-history/test-history.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./Components/landing-page/landing-page.component').then(m=>m.LandingPageComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./Components/login/login.component').then(m=>m.LoginComponent)
  },
  {
    path: 'signup',
    loadComponent: () => import('./Components/signup/signup.component').then(m=>m.SignupComponent)
  },
  {path: 'candidate',
    loadComponent: () => import('./Components/candidate-home/candidate-home.component').then(m=>m.CandidateHomeComponent),
    canActivate: [authGuard]
    
  },
  {
    path: 'startexam',
    loadComponent: () => import('./Components/start-exam/start-exam.component').then(m => m.StartExamComponent),
    canActivate: [authGuard]
  },
  {
    path: 'exam',
    loadComponent: () => import('./Components/exam/exam.component').then(m => m.ExamComponent),
    canActivate: [authGuard]
  },
  {
    path: 'result',
    loadComponent: () => import('./Components/result/result.component').then(m => m.ResultComponent),
    canActivate: [authGuard]
  },
  {path:'test-history'
    ,component:TestHistoryComponent
  },
  { path: '**', component: PageNotFoundComponent }
];
