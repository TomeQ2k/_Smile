import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { NotificationsResponse } from './responses/notifications-response';
import { NotificationService } from '../services/notification.service';

@Injectable()
export class NotificationsResolver implements Resolve<NotificationsResponse> {
  constructor(private router: Router, private notificationService: NotificationService,
              private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<NotificationsResponse> {
    if (!route.queryParams.createMode) {
      return this.notificationService.fetchNotifications().pipe(
        catchError(() => {
          this.notifier.push('Error occurred during loading data', 'error');
          this.router.navigate(['']);

          return of(null);
        }),
      );
    }

    return of(null);
  }
}
