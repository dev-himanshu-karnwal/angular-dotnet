import { Component, inject, signal } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { FormValidationService } from '../../services/form-validation.service';
import { ButtonSpinnerComponent } from '../../../../shared/components/loaders/button-spinner/button-spinner.component';

@Component({
  selector: 'app-auth-login',
  standalone: true,
  imports: [RouterModule, ButtonSpinnerComponent],
  templateUrl: './login.page.html',
  styleUrls: ['./../../../../../styles/auth.styles.css'],
})
export class LoginPage {
  private readonly toastService = inject(ToastService);
  private readonly formValidation = inject(FormValidationService);
  private readonly authService = inject(AuthService);
  private router = inject(Router);

  readonly isLoading = signal(false);

  login(email: string, password: string) {
    if (!this.formValidation.validateLoginForm(email, password)) return;

    this.isLoading.set(true);

    this.authService.login({ email, password }).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.toastService.success('Logged in successfully');
        this.router.navigate(['/dashboard']);
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.toastService.error(err.error.message || 'Something went wrong');
      },
    });
  }

  get formErrors(): Record<string, string | undefined> {
    return this.formValidation.formErrors();
  }
}
