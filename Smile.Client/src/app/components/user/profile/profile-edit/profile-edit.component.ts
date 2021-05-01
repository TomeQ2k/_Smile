import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
import { AuthValidationType } from 'src/app/enums/auth-validation-type.enum';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { UserProfile } from 'src/app/models/domain/profile/user-profile';
import { ProfileResponse } from 'src/app/resolvers/responses/profile-response';
import { AuthService } from 'src/app/services/auth.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { ProfileService } from 'src/app/services/profile.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;

  changeUsernameForm: FormGroup;
  changePasswordForm: FormGroup;
  changeEmailForm: FormGroup;

  profileResponse: ProfileResponse;
  currentUser: UserProfile;

  constants = constants;

  constructor(private profileService: ProfileService, private formBuilder: FormBuilder, public formHelper: FormHelper,
    private authService: AuthService, private router: Router, private notifier: Notifier,
    private route: ActivatedRoute, private listener: ListenerService) { }

  ngOnInit(): void {
    this.createChangeUsernameForm();
    this.createChangePasswordForm();
    this.createChangeEmailForm();

    this.subscribeData();
  }

  public changeUsername() {
    if (this.changeUsernameForm.valid) {
      const request = Object.assign({}, this.changeUsernameForm.value);

      this.profileService.changeUsername(request).subscribe(() => {
        this.notifier.push('Username changed', 'success');

        this.authService.currentUser.username = request.newUsername;
        this.currentUser.username = request.newUsername;

        this.listener.changeCurrentUser(this.authService.currentUser);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/profile']);
      }, () => {
        this.formHelper.resetForm(this.changeUsernameForm);
      });
    }
  }

  public changePassword() {
    if (this.changePasswordForm.valid) {
      const request = Object.assign({}, this.changePasswordForm.value);

      this.profileService.changePassword(request).subscribe(() => {
        this.notifier.push('Password changed', 'success');
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/profile']);
      }, () => {
        this.formHelper.resetForm(this.changePasswordForm);
      });
    }
  }

  public sendChangeEmailCallback() {
    if (this.changeEmailForm.valid) {
      const request = Object.assign({}, this.changeEmailForm.value);

      this.profileService.sendChangeEmailCallback(request).subscribe(() => {
        this.notifier.push(`Change email token was sent to ${request.newEmail}`, 'info');
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/profile']);
      }, () => {
        this.formHelper.resetForm(this.changeEmailForm);
      });
    }
  }

  public loadAvatar(file: File) {
    if (file) {
      if (file.size / 1024 / 1024 <= constants.maxFileSize) {
        const reader = new FileReader();
        reader.readAsDataURL(file);

        reader.onload = () => {
          const url = reader.result.toString();
          this.currentUser.photoUrl = url;

          this.profileService.setAvatar(file).subscribe(() => {
            this.notifier.push('Avatar set', 'info');

            this.authService.currentUser.photoUrl = url;
            this.listener.changeCurrentUser(this.authService.currentUser);
          }, error => {
            this.notifier.push(error, 'error');
          });
        };

        this.fileInput.nativeElement.value = '';
      } else {
        this.notifier.push(`Maximum file size is ${constants.maxFileSize} MB`, 'warning');
      }
    }
  }

  public deleteAvatar() {
    if (this.currentUser.photoUrl) {
      this.profileService.deleteAvatar().subscribe(() => {
        this.notifier.push('Avatar deleted', 'info');

        this.currentUser.photoUrl = null;
        this.authService.currentUser.photoUrl = null;
        this.listener.changeCurrentUser(this.authService.currentUser);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => this.profileResponse = data.profileResponse);

    if (!this.profileResponse.isSucceeded) {
      this.notifier.push('User profile loading failed', 'error');
      this.router.navigate(['']);
    } else {
      this.currentUser = this.profileResponse.userProfile;
    }
  }

  private createChangeUsernameForm() {
    this.changeUsernameForm = this.formBuilder.group({
      newUsername: ['',
        [Validators.required, Validators.minLength(constants.minUsernameLength),
        Validators.maxLength(constants.maxUsernameLength), Validators.pattern(/^\S+$/)],
        this.checkUsernameAvailability.bind(this)]
    });
  }

  private createChangePasswordForm() {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(constants.minPasswordLength),
      Validators.maxLength(constants.maxPasswordLength), Validators.pattern(/^\S+$/)]]
    });
  }

  private createChangeEmailForm() {
    this.changeEmailForm = this.formBuilder.group({
      newEmail: ['', [Validators.required, Validators.email],
        this.checkEmailAvailability.bind(this)],
    });
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
