import { Component, signal } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs';

const FORM_HEADINGS: Record<string, string> = {
  login: 'Login to your account',
  signup: 'Signup to your account',
};

@Component({
  selector: 'app-auth-layout',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './auth-layout.component.html',
  styleUrls: ['./../../../styles/auth.styles.css'],
})
export class AuthLayoutComponent {
  currentRoute = signal<string>('');

  constructor(private router: Router) {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => {
        if (event instanceof NavigationEnd) {
          const route = Object.keys(FORM_HEADINGS).find((key) =>
            event.urlAfterRedirects.includes(key)
          );
          this.currentRoute.set(route || 'login');
        }
      });
  }

  get formHeading(): string {
    return FORM_HEADINGS[this.currentRoute()];
  }
}
