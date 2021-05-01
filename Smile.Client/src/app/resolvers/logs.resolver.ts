import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { LogsResponse } from './responses/logs-response';
import { LogsRequest } from './requests/logs-request';
import { LogsService } from '../services/logs.service';

@Injectable()
export class LogsResolver implements Resolve<PaginatedResult<LogsResponse>> {
  constructor(private router: Router, private logsService: LogsService, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<LogsResponse>> {
    return this.logsService.getLogs(new LogsRequest()).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
