import { APP_INITIALIZER, ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import {
  PublicClientApplication,
  InteractionType
} from '@azure/msal-browser';
import {
  MSAL_INSTANCE,
  MsalService,
  MsalGuard,
  MsalInterceptor,
  MSAL_INTERCEPTOR_CONFIG,
  MsalModule
} from '@azure/msal-angular';
import { MSALInterceptorConfigFactory } from './libs/interceptors/msal.interceptor';
import { MSALInstanceFactory } from './libs/msal-instance-factory/msal-instance-factory';
import { CustInterceptor } from './libs/interceptors/custome-interceptor';

export function initializeMsal(msalService: MsalService) {
  return async () => {
    await msalService.instance.initialize();
    await msalService.instance.handleRedirectPromise();

    const accounts = msalService.instance.getAllAccounts();
    if (accounts.length > 0) {
      msalService.instance.setActiveAccount(accounts[0]);
    }
  };
}

  export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withComponentInputBinding()),
    provideHttpClient( withInterceptorsFromDi()),
    provideAnimations(),
    importProvidersFrom(MsalModule),
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    MsalService,
    MsalGuard,
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CustInterceptor,
      multi: true
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initializeMsal,
      deps: [MsalService],
      multi: true
    }
  ]
};
