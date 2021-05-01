import { Log } from 'src/app/models/domain/log/log';
import { BaseResponse } from './base-response';

export class LogsResponse extends BaseResponse {
  logs: Log[];
}
