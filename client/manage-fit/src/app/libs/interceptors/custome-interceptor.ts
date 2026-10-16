import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';

import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { from, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class CustInterceptor implements HttpInterceptor {

  constructor(private msalService: MsalService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    // 1. check if request matches protected API
    const account = this.msalService.instance.getActiveAccount();

    if (!account) {
      console.warn('No active MSAL account');
      return next.handle(req);
    }

    // 2. get token silently
    return from(
      this.msalService.instance.acquireTokenSilent({
        account,
        scopes: [
          'api://c161d7a8-dbeb-4262-8de8-e3529f173c69/access_as_user'
        ]
      })
    ).pipe(
      switchMap(result => {

        const token = result.accessToken;

        const authReq = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });

        return next.handle(authReq);
      })
    );
  }
}