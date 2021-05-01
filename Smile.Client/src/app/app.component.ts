import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserAuth } from './models/domain/auth/user-auth';
import { AuthService } from './services/auth.service';
import { FriendService } from './services/friend.service';
import { ListenerService } from './services/listener.service';
import { Messenger } from './services/messenger.service';
import { NotificationService } from './services/notification.service';
import { Notifier } from './services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from './services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService, private signalr: Signalr, private listener: ListenerService, private notifier: Notifier,
    private messenger: Messenger, private notificationService: NotificationService, private friendService: FriendService) { }

  ngOnInit() {
    this.validateAuthorization();

    this.authService.refreshUserData();

    this.authService.currentLoggedIn.subscribe(isLoggedIn => {
      if (isLoggedIn) {
        this.subscribeSignalr();
      }
    });

    if (this.authService.isLoggedIn()) {
      this.signalr.startConnection();
      this.signalr.subscribeAction(SIGNALR_ACTIONS.REFRESH_USER_DATA, () => this.authService.refreshUserData());
    }
  }

  private validateAuthorization() {
    const token = localStorage.getItem('token');
    const user: UserAuth = JSON.parse(localStorage.getItem('user'));

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }

    if (user) {
      this.authService.currentUser = user;
      this.listener.changeCurrentUser(user);
    }

    if (token && user && this.validateTokenExpiration(token)) {
      this.authService.isLoggedInEmit();
    }
  }

  private validateTokenExpiration(token: any): boolean {
    if (this.jwtHelper.isTokenExpired(token)) {
      this.authService.logout();
      this.notifier.push('Authorization token expired. Please sign in again', 'warning');

      return false;
    }

    return true;
  }

  private subscribeSignalr() {
    this.signalr.startConnection();
    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_RECEIVED, () => this.messenger.countUnreadMessages());
    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_NOTIFICATION_SENT, () => this.notificationService.countUnreadNotifications());
    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_FRIEND_INVITED, () => this.friendService.countFriendInvites());
    this.signalr.subscribeAction(SIGNALR_ACTIONS.REFRESH_USER_DATA, () => this.authService.refreshUserData());
  }
}
