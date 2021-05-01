import { Report } from 'src/app/models/domain/support/report';
import { BaseResponse } from './base-response';

export class ReportResponse extends BaseResponse {
  report: Report;
}
