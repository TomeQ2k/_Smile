import { NotificationType } from 'src/app/enums/notification-type.enum';

export interface Notification {
  id: string;
  message: string;
  dateSent: Date;
  userId: string;
  isRead: boolean;
  type: NotificationType;
}
