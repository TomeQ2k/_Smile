import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SortType } from 'src/app/enums/sort-type.enum';
import { User } from 'src/app/models/domain/user/user';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { UsersRequest } from 'src/app/resolvers/requests/users-request';
import { AuthService } from 'src/app/services/auth.service';
import { FriendService } from 'src/app/services/friend.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { UserService } from 'src/app/services/user.service';
import { colors } from 'src/environments/environment';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users: User[];
  pagination: Pagination;

  filtersForm: FormGroup;

  usersRequest = new UsersRequest();

  colors = colors;

  sortType = SortType;

  constructor(private userService: UserService, private route: ActivatedRoute, private formBuilder: FormBuilder,
    private notifier: Notifier, private listener: ListenerService, public authService: AuthService, private signalr: Signalr,
    private friendService: FriendService) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();

    this.subscribeSignalr();
  }

  public onFriendInvited(emitter: PutEmitter) {
    if (emitter.updated) {
      const friendIndex = this.users.findIndex(u => u.id === emitter.object);
      this.users[friendIndex] = { ...this.users[friendIndex], isFriend: true, isFriendAccepted: false, isCurrentUserSender: true };
    }
  }

  public onFriendAccepted(emitter: PutEmitter) {
    if (emitter.updated) {
      const userIndex = this.users.findIndex(u => u.id === emitter.object);
      this.users[userIndex] = { ...this.users[userIndex], isFriend: true, isFriendAccepted: true };
      this.friendService.decrementCurrentFriendInvitesCount();
    }
  }

  public onFriendDenied(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      const friendIndex = this.users.findIndex(u => u.id === emitter.objectId);
      this.users[friendIndex] = { ...this.users[friendIndex], isFriend: false, isFriendAccepted: false, isCurrentUserSender: true };
      this.friendService.decrementCurrentFriendInvitesCount();
    }
  }

  public onFriendDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      const userIndex = this.users.findIndex(u => u.id === emitter.objectId);
      this.users[userIndex] = { ...this.users[userIndex], isFriend: false, isFriendAccepted: false, isCurrentUserSender: false };
      this.friendService.decrementCurrentFriendInvitesCount();
    }
  }

  public onFiltersChanged() {
    this.usersRequest.pageNumber = 1;
    this.usersRequest = Object.assign(this.usersRequest, this.filtersForm.value);
    this.filterUsers();
  }

  public resetFilters() {
    this.usersRequest = new UsersRequest();
    this.createFiltersForm();
    this.filterUsers();
  }

  public onScroll() {
    if (this.users.length < this.pagination.totalItems) {
      this.usersRequest.pageNumber++;
      this.filterUsers(true);
    }
  }

  private filterUsers(onScroll = false) {
    this.userService.getUsers(this.usersRequest).subscribe(response => {
      const users = response.result.users;
      this.users = onScroll ? [...this.users, ...users] : users;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      username: [''],
      sortType: [SortType.Descending],
      onlyAdmin: [false]
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.users = data.usersResponse.result.users;
      this.pagination = data.usersResponse.pagination;
    });

    this.listener.currentUsersChanged.subscribe(usersChanged => {
      if (usersChanged) {
        this.filterUsers();
      }
    });
  }

  private subscribeSignalr() {
    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_FRIEND_INVITED, values => {
      if (this.authService.isLoggedIn()) {
        const friendToInviteIndex = this.users.findIndex(u => u.id === values[0].senderId);
        this.users[friendToInviteIndex] = {
          ...this.users[friendToInviteIndex], isFriend: true,
          isFriendAccepted: false, isCurrentUserSender: false
        };

        this.friendService.countFriendInvites();
      }
    });

    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_FRIEND_RECEIVED, values => {
      if (this.authService.isLoggedIn()) {
        const { senderId, recipientId, accepted } = values[0];

        if (accepted) {
          const friendToAcceptIndex = this.users.findIndex(u => u.id === senderId || u.id === recipientId);
          this.users[friendToAcceptIndex] = {
            ...this.users[friendToAcceptIndex], isFriend: true,
            isFriendAccepted: true, isCurrentUserSender: false
          };
        } else {
          const friendToDeleteIndex = this.users.findIndex(u => u.id === senderId || u.id === recipientId);
          this.users[friendToDeleteIndex] = {
            ...this.users[friendToDeleteIndex], isFriend: false,
            isFriendAccepted: false, isCurrentUserSender: false
          };

          this.friendService.decrementCurrentFriendInvitesCount();
        }
      }
    });

    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_FRIEND_DELETED, values => {
      if (this.authService.isLoggedIn()) {
        const friendToDeleteIndex = this.users.findIndex(u => u.id === values[0]);
        this.users[friendToDeleteIndex] = {
          ...this.users[friendToDeleteIndex], isFriend: false,
          isFriendAccepted: false, isCurrentUserSender: false
        };

        this.friendService.countFriendInvites();
      }
    });
  }
}
