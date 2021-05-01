import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { ReportResponse } from './responses/report-response';
import { SupportService } from '../services/support.service';

@Injectable()
export class ReportResolver implements Resolve<ReportResponse> {
  constructor(private router: Router, private supportService: SupportService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<ReportResponse> {
    return this.supportService.getReport(route.params.reportId).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
