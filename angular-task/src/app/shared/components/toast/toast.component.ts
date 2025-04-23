import { Component, inject } from '@angular/core';
import { ToastService } from '../../../core/services/toast.service';

/**
 * Toast component
 *
 * This component displays toast notifications in the application.
 * It shows success, error and info messages that automatically disappear after a set duration.
 *
 * @example
 * ```html
 * <app-toast></app-toast>
 * ```
 */
@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css',
})
export class ToastComponent {
  // Service for managing toast notifications
  private readonly toastService = inject(ToastService);

  // Observable of current toast notifications
  toasts = this.toastService.toasts;

  // Method to remove a toast by its ID
  removeToast = this.toastService.removeToast;
}
