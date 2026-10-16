import { PublicClientApplication } from "@azure/msal-browser";

export function MSALInstanceFactory() {
  return new PublicClientApplication({
    auth: {
      clientId: '52cfc2c1-8d55-4273-a13a-e8c36af61f08',
      authority: 'https://login.microsoftonline.com/f3910c39-2790-4371-a675-b6fb202ef251',
      redirectUri: 'http://localhost:4200/clients'
    }
  });
}