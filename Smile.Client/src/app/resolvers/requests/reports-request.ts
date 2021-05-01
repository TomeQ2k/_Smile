import { PaginationRequest } from './pagination-request';

export class ReportsRequest extends PaginationRequest {
  reportStatus: number;
  sortType = 0;
  reportType: number;
  reporterName: string;
}
