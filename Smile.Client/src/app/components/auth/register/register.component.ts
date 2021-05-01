import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { map, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { AuthValidationType } from 'src/app/enums/auth-validation-type.enum';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constants = constants;

  constructor(private authService: AuthService, private notifier: Notifier, private formBuilder: FormBuilder,
    private router: Router, public formHelper: FormHelper) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  public register() {
    if (this.registerForm.valid) {
      const userRegister = Object.assign({}, this.registerForm.value);

      this.authService.register(userRegister).subscribe(() => {
        this.notifier.push('Account has been created. Activation account email has been sent', 'success');
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/auth']);
      }, () => {
        this.formHelper.resetForm(this.registerForm);
      });
    }
  }

  private createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email],
        this.checkEmailAvailability.bind(this)],
      username: ['', [Validators.required, Validators.minLength(constants.minUsernameLength),
      Validators.maxLength(constants.maxUsernameLength), Validators.pattern(/^\S+$/)],
        this.checkUsernameAvailability.bind(this)],
      password: ['', [Validators.required, Validators.minLength(constants.minPasswordLength),
      Validators.maxLength(constants.maxPasswordLength), Validators.pattern(/^\S+$/)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: [
        this.passwordMatchValidator
      ]
    });
  }

  private passwordMatchValidator(formGroup: FormGroup) {
    const password: string = formGroup.get('password').value;
    const confirmPassword: string = formGroup.get('confirmPassword').value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword').setErrors({ passwordMismatch: true });
    }
  }

  private getAuthValidations(authValidationType: AuthValidationType, content: string, error: object) {
    return this.authService.getAuthValidations(authValidationType, content).pipe(
      map((response: any) => {
        return response.isAvailable ? null : error;
      })
    );
  }

  private checkEmailAvailability(control: AbstractControl) {
    return control.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap(value => {
        return this.getAuthValidations(AuthValidationType.Email, value, { emailTaken: true }).pipe(
          map(response => {
            control.setErrors(response);
          })
        );
      })
    );
  }

  private checkUsernameAvailability(control: AbstractControl) {
    return control.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap(value => {
        return this.getAuthValidations(AuthValidationType.Username, value, { usernameTaken: true }).pipe(
          map(response => {
            control.setErrors(response);
          })
        );
      })
    );
  }
}
