import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { UserService } from '../services/user.service';
import { UserResponse } from './responses/user-response';

@Injectable()
export class UserResolver implements Resolve<UserResponse> {
  constructor(private router: Router, private userService: UserService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<UserResponse> {
    return this.userService.getUser(route.params.userId).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
