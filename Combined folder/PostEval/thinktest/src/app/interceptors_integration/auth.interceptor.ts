import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let Token: string = '';
    if(typeof window.sessionStorage !== 'undefined' && window.sessionStorage) {
      const loggedUser = sessionStorage.getItem('loggedUser');
      if(loggedUser){
        const userData = JSON.parse(loggedUser);
        Token = userData.token;
      }
    }
    
    if (Token) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${Token}`)
      });
      console.log('Token being sent:', Token);
      console.log(req);
      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}