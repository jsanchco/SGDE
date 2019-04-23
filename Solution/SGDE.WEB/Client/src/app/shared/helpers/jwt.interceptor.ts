import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { StorageService } from '../services/storage.service';

@Injectable()

export class JwtInterceptor implements HttpInterceptor {

  constructor(private storageService: StorageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    const currentSession = this.storageService.getCurrentSession();
    if (currentSession && currentSession.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentSession.token}`
        }
      });
    }

    return next.handle(request);
  }
}
