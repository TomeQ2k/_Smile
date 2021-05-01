import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormHelper {

  public resetForm(formGroup: FormGroup) {
    const controls = formGroup.controls;

    formGroup.reset();

    for (const key in controls) {
      controls[key].setErrors(null);
    }
  }

  public getErrorMessage(formGroup: FormGroup, field: string) {
    if (formGroup.get(field).hasError('required')) {
      return 'Field is required';
    }
    if (formGroup.get(field).hasError('minlength')) {
      return `Field must exceed ${formGroup.get(field).errors.minlength.requiredLength} characters`;
    }
    if (formGroup.get(field).hasError('maxlength')) {
      return `Field cannot exceed ${formGroup.get(field).errors.maxLength.requiredLength} characters`;
    }
    if (formGroup.get(field).hasError('email')) {
      return 'Invalid email address format';
    }
    if (formGroup.get(field).hasError('pattern')) {
      return 'Invalid format';
    }
    if (formGroup.get(field).hasError('passwordMismatch')) {
      return 'Passwords do not match';
    }
    if (formGroup.get(field).hasError('emailTaken')) {
      return 'Email address already exists';
    }
    if (formGroup.get(field).hasError('usernameTaken')) {
      return 'Username already exists';
    }
  }
}
