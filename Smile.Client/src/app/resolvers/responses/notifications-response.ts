import { Notification } from 'src/app/models/domain/notification/notification';
import { BaseResponse } from './base-response';

export class NotificationsResponse extends BaseResponse {
  notifications: Notification[];
}
