import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { LogsRequest } from '../resolvers/requests/logs-request';
import { LogsResponse } from '../resolvers/responses/logs-response';

@Injectable({
  providedIn: 'root'
})
export class LogsService {
  private readonly logsApiUrl = environment.apiUrl + 'log/';

  constructor(private httpClient: HttpClient) { }

  public getLogs(logsRequest: LogsRequest) {
    const paginatedResult: PaginatedResult<LogsResponse> = new PaginatedResult<LogsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', logsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', logsRequest.pageSize.toString());

    if (logsRequest.minDate) {
      httpParams = httpParams.append('minDate', logsRequest.minDate.toISOString());
    }

    if (logsRequest.maxDate) {
      httpParams = httpParams.append('maxDate', logsRequest.maxDate.toISOString());
    }

    if (logsRequest.level) {
      httpParams = httpParams.append('level', logsRequest.level);
    }

    if (logsRequest.message) {
      httpParams = httpParams.append('message', logsRequest.message);
    }

    if (logsRequest.url) {
      httpParams = httpParams.append('url', logsRequest.url);
    }

    if (logsRequest.action) {
      httpParams = httpParams.append('action', logsRequest.action);
    }

    httpParams = httpParams.append('sortType', logsRequest.sortType.toString());

    return this.httpClient.get<LogsResponse>(this.logsApiUrl + 'filter', { observe: 'response', params: httpParams })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }
}
