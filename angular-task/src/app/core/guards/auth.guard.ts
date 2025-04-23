import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { LoadingService } from '../services/loading.service';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
): Observable<boolean> => {
  const router = inject(Router);
  const authService = inject(AuthService);
  const loadingService = inject(LoadingService);

  // Show loading state
  loadingService.show();

  // Check if the user is already authenticated
  const user = authService.user();
  if (user) {
    return of(true);
  }

  // Make an HTTP request to check authentication
  return authService.fetchAuthUser().pipe(
    // Hide loading state on success
    tap(() => {
      loadingService.hide();
    }),
    // Allow access if the request is successful
    map(() => true),
    // Hide loading state and redirect to login on error
    catchError(() => {
      loadingService.hide();
      router.navigate(['/auth', 'login']);
      return of(false); // Deny access
    })
  );
};
