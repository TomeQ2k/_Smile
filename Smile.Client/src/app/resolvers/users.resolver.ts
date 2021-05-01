import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { UsersResponse } from './responses/users-response';
import { UserService } from '../services/user.service';
import { UsersRequest } from './requests/users-request';

@Injectable()
export class UsersResolver implements Resolve<PaginatedResult<UsersResponse>> {
  constructor(private router: Router, private userService: UserService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<UsersResponse>> {
    return this.userService.getUsers(new UsersRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
