import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Notification } from 'src/app/models/domain/notification/notification';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { colors } from 'src/environments/environment';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
  notifications: Notification[];

  greenColor = colors.greenColor;
  destructiveColor = colors.destructiveColor;

  constructor(private notificationService: NotificationService, private route: ActivatedRoute, private notifier: Notifier,
    private signalr: Signalr, private authService: AuthService) { }

  ngOnInit(): void {
    this.subscribeData();

    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_NOTIFICATION_SENT, values => {
      if (this.authService.isLoggedIn()) {
        this.notifications.unshift(values[0]);
      }
    });
  }

  public markAsRead() {
    this.notificationService.markNotificationsAsRead().subscribe(() => {
      for (const notification of this.notifications) {
        notification.isRead = true;
      }

      this.notificationService.changeCurrentUnreadNotificationsCount(0);
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public removeNotification(notificationId: string) {
    this.notificationService.removeNotification(notificationId).subscribe(() => {
      this.notifications = this.notifications.filter(n => n.id !== notificationId);
      this.notificationService.decrementCurrentUnreadNotificationsCount();
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public clearAll() {
    this.notificationService.clearNotifications().subscribe(() => {
      this.notifications = [];
      this.notificationService.changeCurrentUnreadNotificationsCount(0);
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private subscribeData = () => {
    this.route.data.subscribe(data => this.notifications = data.notificationsResponse.notifications);
  }
}
