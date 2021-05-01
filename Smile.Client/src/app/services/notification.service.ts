import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly notificationApiUrl = environment.apiUrl + 'notification/';

  private unreadNotificationsCount = new BehaviorSubject<number>(0);
  currentUnreadNotificationsCount = this.unreadNotificationsCount.asObservable();

  constructor(private httpClient: HttpClient) { }

  public fetchNotifications() {
    return this.httpClient.get<any>(this.notificationApiUrl + 'fetch');
  }

  public markNotificationsAsRead() {
    return this.httpClient.put(this.notificationApiUrl + 'markAsRead', {});
  }

  public removeNotification(notificationId: string) {
    return this.httpClient.delete(this.notificationApiUrl + 'remove', { params: { notificationId } });
  }

  public clearNotifications() {
    return this.httpClient.delete(this.notificationApiUrl + 'clear');
  }

  public countUnreadNotifications() {
    return this.httpClient.get<any>(this.notificationApiUrl + 'unread/count').subscribe(response => {
      this.changeCurrentUnreadNotificationsCount(response.unreadNotificationsCount);
    });
  }

  public changeCurrentUnreadNotificationsCount(unreadNotificationsCount: number) {
    this.unreadNotificationsCount.next(unreadNotificationsCount);
  }

  public decrementCurrentUnreadNotificationsCount() {
    if (this.unreadNotificationsCount.value > 0) {
      this.unreadNotificationsCount.next(this.unreadNotificationsCount.value - 1);
    }
  }
}
