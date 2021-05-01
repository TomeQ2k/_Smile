import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { ReportsResponse } from './responses/reports-response';
import { SupportService } from '../services/support.service';
import { ReportsRequest } from './requests/reports-request';

@Injectable()
export class ReportsResolver implements Resolve<PaginatedResult<ReportsResponse>> {
  constructor(private router: Router, private supportService: SupportService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<ReportsResponse>> {
    const reportsRequest = new ReportsRequest();
    return !route.data.isAdmin ? this.supportService.fetchReports(reportsRequest).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    ) : this.supportService.fetchAllReports(reportsRequest).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
