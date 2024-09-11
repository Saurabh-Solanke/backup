import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './comp/shared/login/login.component';
import { SignupComponent } from './comp/shared/signup/signup.component';
import { NavbarComponent } from './comp/shared/navbar/navbar.component';
import { FooterComponent } from './comp/shared/footer/footer.component';
import { LandingPageComponent } from './comp/shared/landing-page/landing-page.component';
import { PageNotFoundComponent } from './comp/shared/page-not-found/page-not-found.component';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideRouter, RouterLink, RouterOutlet } from '@angular/router';

import { provideToastr } from 'ngx-toastr';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';

import {routes} from './app-routing.module'
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    NavbarComponent,
    FooterComponent,
    LandingPageComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    CommonModule,
    RouterOutlet,
    RouterLink
  ],
  providers: [provideHttpClient(withInterceptorsFromDi()), provideClientHydration(), provideHttpClient(),
    provideToastr(), 
    {
      provide:HTTP_INTERCEPTORS,
      useClass:AuthInterceptor,
      multi:true  
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
