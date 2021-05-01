import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GroupService } from '../services/group.service';
import { Notifier } from '../services/notifier.service';
import { UserGroupsResponse } from './responses/user-groups-response';

@Injectable()
export class UserGroupsResolver implements Resolve<UserGroupsResponse> {
  constructor(private router: Router, private groupService: GroupService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<UserGroupsResponse> {
    if (route.data.isProfile) {
      return this.groupService.fetchUserGroups().pipe(
        catchError(() => {
          this.notifier.push('Error occurred during loading data', 'error');
          this.router.navigate(['']);

          return of(null);
        }),
      );
    }
  }
}
