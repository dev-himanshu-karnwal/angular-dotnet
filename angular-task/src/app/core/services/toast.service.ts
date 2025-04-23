import { Injectable, signal } from '@angular/core';
import { type Toast } from '../../shared/components/toast/toast.model';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private _toasts = signal<Toast[]>([]);
  public toasts = this._toasts.asReadonly();

  private showToast(
    message: string,
    type: Toast['type'],
    duration: number = 3000
  ) {
    const id = Date.now();
    const newToast: Toast = { id, message, type };

    this._toasts.update((currentToasts) => [newToast, ...currentToasts]);

    setTimeout(() => this.removeToast(id), duration);
  }

  removeToast(id: number) {
    const toastElement = document.querySelector(`[data-toast-id="${id}"]`);
    if (!toastElement) return;

    toastElement.classList.add('removing');

    setTimeout(() => {
      this._toasts.update((currentToasts) =>
        currentToasts.filter((toast) => toast.id !== id)
      );
    }, 300);
  }

  success(message: string, duration?: number) {
    this.showToast(message, 'success', duration);
  }

  error(message: string, duration?: number) {
    this.showToast(message, 'error', duration);
  }

  info(message: string, duration?: number) {
    this.showToast(message, 'info', duration);
  }

  warn(message: string, duration?: number) {
    this.showToast(message, 'warn', duration);
  }
}
