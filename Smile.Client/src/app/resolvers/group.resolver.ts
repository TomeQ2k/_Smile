import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GroupService } from '../services/group.service';
import { Notifier } from '../services/notifier.service';
import { GroupResponse } from './responses/group-response';

@Injectable()
export class GroupResolver implements Resolve<GroupResponse> {
  constructor(private router: Router, private groupService: GroupService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<GroupResponse> {
    return this.groupService.getGroup(route.params.groupId).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
