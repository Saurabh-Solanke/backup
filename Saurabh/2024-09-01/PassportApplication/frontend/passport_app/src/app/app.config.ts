import { ApplicationConfig } from '@angular/core';
import { provideRouter, withRouterConfig } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './passport.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes,withRouterConfig({
    paramsInheritanceStrategy: 'always'
  })),provideHttpClient(withFetch(),withInterceptorsFromDi()),{
    provide:HTTP_INTERCEPTORS,
    useClass:AuthInterceptor,
    multi:true
  }]
};
