import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';

import { HTTP_INTERCEPTORS, withInterceptorsFromDi } from '@angular/common/http';

import { AuthInterceptor } from './interceptors_integration/auth.interceptor';
import { provideToastr, ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


export const appConfig: ApplicationConfig = {

  
  providers: [provideHttpClient(withInterceptorsFromDi()), provideRouter(routes), provideClientHydration(), provideHttpClient(),BrowserAnimationsModule,
    provideToastr(), 
    {
      provide:HTTP_INTERCEPTORS,
      useClass:AuthInterceptor,
      multi:true  
    }
  ],
};
