import { MsalInterceptorConfiguration } from "@azure/msal-angular";
import { InteractionType } from "@azure/msal-browser";

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();

  protectedResourceMap.set(
    'https://localhost:7003/api/',
    ['api://c161d7a8-dbeb-4262-8de8-e3529f173c69/access_as_user']
  );

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}