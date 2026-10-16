import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClient } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { EventMessage, EventType } from '@azure/msal-browser';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    MatSlideToggleModule,
    RouterLink,
    RouterLinkActive,
    MatButtonModule,
    MatMenuModule,
    MatDividerModule,
    MatIconModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'manage-fit';
  isLoggedIn = false;

  readonly navItems = [
    { label: 'Clients', path: '/clients' },
    { label: 'Nutrition', path: '/nutritions' },
    { label: 'Workouts', path: '/workouts' },
    { label: 'Exercises', path: '/exercises' }
  ];

  get currentItem() {
    return this.navItems.find(item => this.router.url.startsWith(item.path));
  }

  constructor(public http: HttpClient, private msalService: MsalService, private router: Router) {}

  ngOnInit(): void {
    this.updateAuthState();

    this.msalService.instance.addEventCallback((event: EventMessage) => {
      if (
        event.eventType === EventType.LOGIN_SUCCESS ||
        event.eventType === EventType.ACQUIRE_TOKEN_SUCCESS ||
        event.eventType === EventType.ACTIVE_ACCOUNT_CHANGED
      ) {
        this.updateAuthState();
      }

      if (event.eventType === EventType.LOGOUT_SUCCESS || event.eventType === EventType.LOGOUT_FAILURE) {
        this.isLoggedIn = false;
        this.router.navigateByUrl('/login');
      }
    });
  }

  private updateAuthState(): void {
    this.isLoggedIn = this.msalService.instance.getAllAccounts().length > 0;
  }
}
