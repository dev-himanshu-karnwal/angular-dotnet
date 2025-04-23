import { Component, inject, signal } from '@angular/core';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { lucideArrowRightFromLine } from '@ng-icons/lucide';

import { RouterOutlet } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-panel-layout',
  standalone: true,
  imports: [RouterOutlet, NgIcon],
  viewProviders: [provideIcons({ lucideArrowRightFromLine })],
  styleUrl: './panel-layout.component.css',
  templateUrl: './panel-layout.component.html',
})
export class PanelLayoutComponent {
  private authService = inject(AuthService);
  isNavbarOpen = signal<boolean>(false);

  toggleNavbar() {
    this.isNavbarOpen.update((prev) => !prev);
  }

  get username(): string {
    return this.authService.user()!.username;
  }
}
