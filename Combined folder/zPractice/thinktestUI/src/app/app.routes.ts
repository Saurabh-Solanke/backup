import { Routes } from '@angular/router';
import { authGuard } from './guards_integration/auth.guard';
import { LandingPageComponent } from './comp/landing-page/landing-page.component';


export const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent
  },
  {
    path: 'login',
    loadComponent: () => import('./comp/login/login.component').then(m=>m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./comp/signup/signup.component').then(m=>m.SignupComponent)
  },
  {
    path: 'home',
    loadComponent: () => import('./comp/homepage/homepage.component').then(m=>m.HomepageComponent),
    canActivate: [authGuard]
  },
  {
    path: 'startexam',
    loadComponent: () => import('./comp/start-exam/start-exam.component').then(m => m.StartExamComponent),
    canActivate: [authGuard]
  },
  {
    path: 'testHistory',
    loadComponent: () => import('./comp/test-history/test-history.component').then(m=>m.TestHistoryComponent),
    canActivate: [authGuard]
  },
  {
    path: 'exam',
    loadComponent: () => import('./comp/exam/exam.component').then(m => m.ExamComponent),
    canActivate: [authGuard]
  },
  {
    path: 'result',
    loadComponent: () => import('./comp/result/result.component').then(m => m.ResultComponent),
    canActivate: [authGuard]
  },
  {
    path: '**',
    loadComponent: () => import('./comp/page-not-found/page-not-found.component').then(m => m.PageNotFoundComponent)
  }
];
