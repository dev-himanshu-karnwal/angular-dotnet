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
  templateUrl: './signup.page.html',
  styleUrls: ['./../../../../../styles/auth.styles.css'],
})
export class SignupPage {
  private readonly toastService = inject(ToastService);
  private readonly formValidation = inject(FormValidationService);
  private readonly authService = inject(AuthService);
  private router = inject(Router);

  readonly isLoading = signal(false);

  signup(username: string, email: string, password: string) {
    if (!this.formValidation.validateSignupForm(username, email, password))
      return;

    this.isLoading.set(true);

    this.authService.signup({ username, email, password }).subscribe({
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
