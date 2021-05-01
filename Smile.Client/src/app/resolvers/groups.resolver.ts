import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PaginatedResult } from '../models/helpers/pagination';
import { GroupService } from '../services/group.service';
import { Notifier } from '../services/notifier.service';
import { GroupsRequest } from './requests/groups-request';
import { GroupsResponse } from './responses/groups-response';

@Injectable()
export class GroupsResolver implements Resolve<PaginatedResult<GroupsResponse>> {
  constructor(private router: Router, private groupService: GroupService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<GroupsResponse>> {
    return this.groupService.fetchGroups(new GroupsRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
