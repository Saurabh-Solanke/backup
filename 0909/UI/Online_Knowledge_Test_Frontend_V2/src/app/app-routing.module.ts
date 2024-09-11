import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './comp/shared/landing-page/landing-page.component';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './comp/shared/login/login.component';
import { SignupComponent } from './comp/shared/signup/signup.component';
import { PageNotFoundComponent } from './comp/shared/page-not-found/page-not-found.component';

export const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: SignupComponent
  },
  {
    path: 'user',
    loadChildren: () => import('./comp/user/user.module').then(m=>m.UserModule),
    canActivate: [authGuard]
  },
  {
    path: 'admin',
    loadChildren: () => import('./comp/admin/admin.module').then(m => m.AdminModule),
    canActivate: [authGuard]
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
