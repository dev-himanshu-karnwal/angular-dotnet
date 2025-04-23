import { AbstractControl } from '@angular/forms';
import { EMAIL_REGEX } from '../../features/auth/services/form-validation.service';

const PHONE_NUMBER_REGEX = /^(\+?[1-9]\d{0,2}[-.\s]?)?(\d{3}[-.\s]?){2}\d{4}$/;

export function emailValidator(control: AbstractControl) {
  return EMAIL_REGEX.test(control.value) ? null : { email: true };
}

export function phoneValidator(control: AbstractControl) {
  return PHONE_NUMBER_REGEX.test(control.value) ? null : { phoneNumber: true };
}
