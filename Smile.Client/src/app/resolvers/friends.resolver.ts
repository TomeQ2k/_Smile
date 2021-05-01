import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { FriendsResponse } from './responses/friends-response';
import { FriendService } from '../services/friend.service';
import { FriendsRequest } from './requests/friends-request';

@Injectable()
export class FriendsResolver implements Resolve<PaginatedResult<FriendsResponse>> {
  constructor(private router: Router, private friendService: FriendService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<FriendsResponse>> {
    return this.friendService.getFriends(new FriendsRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
