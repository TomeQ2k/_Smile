import { ReportList } from 'src/app/models/domain/support/report-list';
import { BaseResponse } from './base-response';

export class ReportsResponse extends BaseResponse {
  reports: ReportList[];
}
