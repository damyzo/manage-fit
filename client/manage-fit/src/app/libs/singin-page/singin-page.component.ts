import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-singin-page',
  standalone: true,
  imports: [],
  templateUrl: './singin-page.component.html',
  styleUrl: './singin-page.component.scss'
})
export class SinginPageComponent {
  constructor(private msalService: MsalService) {}

  login() {
    console.log('Login button clicked');
    this.msalService.loginRedirect();
  }

}
