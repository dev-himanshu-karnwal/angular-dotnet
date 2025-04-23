import { Component, inject } from '@angular/core';
import {
  NavigationError,
  NavigationCancel,
  NavigationEnd,
  NavigationStart,
  RouterOutlet,
  Router,
} from '@angular/router';

import { LoadingService } from './core/services/loading.service';
import { ToastComponent } from './shared/components/toast/toast.component';
import { PageSpinnerComponent } from './shared/components/loaders/full-page-spinner/page-spinner.component';

/**
 * The root component of the application
 *
 * This component is the highest level component in the application and
 * contains the router outlet that renders the application's routes.
 *
 * It also contains the toast component that displays toast messages
 * and the page spinner component that displays a full-page loading indicator
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastComponent, PageSpinnerComponent],
  template: '<router-outlet /> <app-toast/> <app-page-spinner/>',
})
export class AppComponent {
  private readonly loadingService = inject(LoadingService);
  private readonly router = inject(Router);

  /**
   * Constructor that sets up the loading service to show/hide the
   * full-page loading indicator based on router events
   *
   * @param router The router that emits events when the application's
   * routes change
   */
  constructor() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.loadingService.show();
      } else if (
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      ) {
        this.loadingService.hide();
      }
    });
  }
}
