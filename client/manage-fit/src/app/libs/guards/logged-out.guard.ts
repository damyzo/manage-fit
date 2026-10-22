import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';

export const loggedOutGuard: CanActivateFn = () => {
  const msalService = inject(MsalService);
  const router = inject(Router);

  return msalService.instance.getAllAccounts().length > 0
    ? router.createUrlTree(['/clients'])
    : true;
};
