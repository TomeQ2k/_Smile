import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Report } from '../models/domain/support/report';
import { PaginatedResult } from '../models/helpers/pagination';
import { ReportsRequest } from '../resolvers/requests/reports-request';
import { ReportsResponse } from '../resolvers/responses/reports-response';

@Injectable({
  providedIn: 'root'
})
export class SupportService {
  private readonly supportApiUrl = environment.apiUrl + 'support/';

  constructor(private httpClient: HttpClient) { }

  public getReport(reportId: string) {
    return this.httpClient.get<Report>(this.supportApiUrl + 'report/', { params: { reportId } });
  }

  public fetchReports(reportsRequest: ReportsRequest) {
    const paginatedResult: PaginatedResult<ReportsResponse> = new PaginatedResult<ReportsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', reportsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', reportsRequest.pageSize.toString());

    if (reportsRequest.reportStatus) {
      httpParams = httpParams.append('reportStatus', reportsRequest.reportStatus.toString());
    }

    httpParams = httpParams.append('sortType', reportsRequest.sortType.toString());

    return this.httpClient.get<ReportsResponse>(this.supportApiUrl + 'reports', { observe: 'response', params: httpParams })
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

  public fetchAllReports(reportsRequest: ReportsRequest) {
    const paginatedResult: PaginatedResult<ReportsResponse> = new PaginatedResult<ReportsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', reportsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', reportsRequest.pageSize.toString());

    if (reportsRequest.reportStatus) {
      httpParams = httpParams.append('reportStatus', reportsRequest.reportStatus.toString());
    }

    if (reportsRequest.reportType) {
      httpParams = httpParams.append('reportType', reportsRequest.reportType.toString());
    }

    if (reportsRequest.reporterName) {
      httpParams = httpParams.append('reporterName', reportsRequest.reporterName);
    }

    httpParams = httpParams.append('sortType', reportsRequest.sortType.toString());

    return this.httpClient.get<ReportsResponse>(this.supportApiUrl + 'reports/all', { observe: 'response', params: httpParams })
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

  public createReport(subject: string, content: string, files: File[]) {
    const formData = new FormData();

    formData.append('subject', subject);
    formData.append('content', content);

    Array.from(files).map((file) => {
      return formData.append('files', file, file.name);
    });

    return this.httpClient.post(this.supportApiUrl + 'report/create', formData);
  }

  public sendReply(content: string, reportId: string, files: File[]) {
    const formData = new FormData();

    formData.append('content', content);
    formData.append('reportId', reportId);

    Array.from(files).map((file) => {
      return formData.append('files', file, file.name);
    });

    return this.httpClient.post(this.supportApiUrl + 'reply/send', formData, { observe: 'response' });
  }

  public createAnonymousReport(subject: string, content: string, email: string) {
    return this.httpClient.post(this.supportApiUrl + 'report/anonymous/create', { subject, content, email });
  }

  public toggleReportStatus(reportId: string) {
    return this.httpClient.patch(this.supportApiUrl + 'report/toggleStatus', { reportId });
  }

  public deleteReport(reportId: string) {
    return this.httpClient.delete(this.supportApiUrl + 'report/delete', { params: { reportId } });
  }
}
