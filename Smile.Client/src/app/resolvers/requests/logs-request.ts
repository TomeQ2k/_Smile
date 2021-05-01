import { logsPageSize } from 'src/environments/environment';
import { PaginationRequest } from './pagination-request';

export class LogsRequest extends PaginationRequest {
  pageSize = logsPageSize;

  minDate: Date;
  maxDate: Date;
  level: string;
  message: string;
  url: string;
  action: string;

  sortType = 0;
}
