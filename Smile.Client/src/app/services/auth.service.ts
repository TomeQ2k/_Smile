import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthValidationType } from '../enums/auth-validation-type.enum';
import { UserAuth } from '../models/domain/auth/user-auth';
import { ListenerService } from './listener.service';
import { ProfileService } from './profile.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authApiUrl = environment.apiUrl + 'auth/';

  private jwtHelper = new JwtHelperService();

  currentUser: UserAuth;
  decodedToken: any;

  private loggedIn = new BehaviorSubject(this.isLoggedIn());
  currentLoggedIn = this.loggedIn.asObservable();

  constructor(private httpClient: HttpClient, private router: Router, private listener: ListenerService,
    private profileService: ProfileService) { }

  public login(user: any) {
    return this.httpClient.post(this.authApiUrl + 'login', user, { observe: 'response' })
      .pipe(
        map((res: any) => {
          const response = res.body;

          if (response && response.isSucceeded) {
            this.initUserData(response);
          }
        })
      );
  }

  public register(user: any) {
    return this.httpClient.post(this.authApiUrl + 'register', user);
  }

  public logout() {
    localStorage.clear();

    this.decodedToken = null;
    this.currentUser = null;

    this.isLoggedInEmit();
    this.router.navigate(['/auth']);
  }

  public checkPermissions(roles: string[]) {
    let isPermitted = false;
    const userRoles = this.decodedToken?.role as string[];

    if (!userRoles) {
      return false;
    }

    for (const role of roles) {
      if (userRoles.includes(role)) {
        isPermitted = true;
        break;
      }
    }

    return isPermitted;
  }

  public confirmAccount(userId: string, token: string) {
    return this.httpClient.get(this.authApiUrl + 'account/confirm', { params: { userId: userId, token }, responseType: 'text' });
  }

  public resetPassword(request: any) {
    return this.httpClient.patch(this.authApiUrl + 'resetPassword', request);
  }

  public sendResetPassword(request: any) {
    return this.httpClient.post(this.authApiUrl + 'resetPassword/send', request);
  }

  public verifyResetPassword(userId: string, token: string) {
    return this.httpClient.get(this.authApiUrl + 'resetPassword/verify', { params: { userId, token } });
  }

  public getAuthValidations(authValidationType: AuthValidationType, content: string) {
    return this.httpClient.get(this.authApiUrl + 'validations', { params: { authValidationType: authValidationType.toString(), content } });
  }

  public isLoggedIn() {
    return !!this.decodedToken;
  }

  public isLoggedInEmit() {
    this.loggedIn.next(this.isLoggedIn());
  }

  public refreshUserData() {
    if (this.isLoggedIn()) {
      this.profileService.refreshUserData().subscribe(response => {
        this.initUserData(response);
      });
    }
  }

  private initUserData(response: any) {
    localStorage.setItem('token', response.token);
    this.decodedToken = this.jwtHelper.decodeToken(response.token);

    this.currentUser = response.user;
    this.listener.changeCurrentUser(this.currentUser);

    this.isLoggedInEmit();
  }
}
