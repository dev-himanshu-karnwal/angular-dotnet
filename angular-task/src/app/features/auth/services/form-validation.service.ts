import { Injectable, signal } from '@angular/core';

export const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
export const PASSWORD_MIN_LENGTH = 6;

@Injectable({
  providedIn: 'root',
})
export class FormValidationService {
  formErrors = signal<Record<string, string | undefined>>({});

  private validateEmail(email: string): boolean {
    return EMAIL_REGEX.test(email);
  }

  private validatePassword(password: string): boolean {
    return password.length >= PASSWORD_MIN_LENGTH;
  }

  private clearErrors() {
    this.formErrors.set({});
  }

  validateSignupForm(
    username: string,
    email: string,
    password: string
  ): boolean {
    this.clearErrors();
    let isValid = true;

    if (username.length < 2) {
      this.formErrors.update((errors) => ({
        ...errors,
        username: 'Username must be at least 2 characters',
      }));
    }

    if (!this.validateEmail(email)) {
      this.formErrors.update((errors) => ({
        ...errors,
        email: 'Please enter a valid email address',
      }));
      isValid = false;
    }

    if (!this.validatePassword(password)) {
      this.formErrors.update((errors) => ({
        ...errors,
        password: 'Password must be at least 6 characters',
      }));
      isValid = false;
    }

    return isValid;
  }

  validateLoginForm(email: string, password: string): boolean {
    this.clearErrors();
    let isValid = true;

    if (!this.validateEmail(email)) {
      this.formErrors.update((errors) => ({
        ...errors,
        email: 'Please enter a valid email address',
      }));
      isValid = false;
    }

    if (!this.validatePassword(password)) {
      this.formErrors.update((errors) => ({
        ...errors,
        password: 'Password must be at least 6 characters',
      }));
      isValid = false;
    }

    return isValid;
  }
}
