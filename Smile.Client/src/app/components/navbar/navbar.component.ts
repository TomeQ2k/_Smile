import { Component, OnInit } from '@angular/core';
import { UserAuth } from 'src/app/models/domain/auth/user-auth';
import { AuthService } from 'src/app/services/auth.service';
import { FriendService } from 'src/app/services/friend.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { NotificationService } from 'src/app/services/notification.service';
import { roles } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean;
  isAdmin: boolean;
  currentUser: UserAuth;

  unreadMessagesCount: number;
  unreadNotificationsCount: number;
  friendInvitesCount: number;

  constructor(private authService: AuthService, private listener: ListenerService, private messenger: Messenger,
    private notificationService: NotificationService, private friendService: FriendService) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  private subscribeData() {
    this.authService.currentLoggedIn.subscribe(loggedIn => {
      this.isLoggedIn = loggedIn;

      if (this.isLoggedIn) {
        this.messenger.countUnreadMessages();
        this.notificationService.countUnreadNotifications();
        this.friendService.countFriendInvites();
      }
    });

    this.listener.currentUser.subscribe(user => {
      this.currentUser = user;
      this.isAdmin = this.authService.checkPermissions(roles.adminRoles);
    });

    this.messenger.currentUnreadMessagesCount.subscribe(unreadMessagesCount => this.unreadMessagesCount = unreadMessagesCount);
    this.notificationService.currentUnreadNotificationsCount.subscribe(
      unreadNotificationsCount => this.unreadNotificationsCount = unreadNotificationsCount);
    this.friendService.currentFriendInvitesCount.subscribe(friendInvitesCount => this.friendInvitesCount = friendInvitesCount);
  }
}
