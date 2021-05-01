import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Friend } from 'src/app/models/domain/community/friend';
import { Pagination } from 'src/app/models/helpers/pagination';
import { FriendsRequest } from 'src/app/resolvers/requests/friends-request';
import { AuthService } from 'src/app/services/auth.service';
import { FriendService } from 'src/app/services/friend.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class FriendsComponent implements OnInit {
  friends: Friend[];
  pagination: Pagination;

  filtersForm: FormGroup;

  friendsRequest = new FriendsRequest();

  currentUserId: string;

  constructor(private friendService: FriendService, private authService: AuthService, private route: ActivatedRoute,
    private formBuilder: FormBuilder, private notifier: Notifier, private listener: ListenerService) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();
  }

  public receiveFriend(senderId: string, recipientId: string, accepted: boolean) {
    this.friendService.receive(senderId, recipientId, accepted).subscribe(res => {
      const response: any = res?.body;
      const { friendAccepted, friend } = response;

      this.notifier.push(`Invite ${(friendAccepted ? 'accepted' : 'ignored')}`, friendAccepted ? 'success' : 'info');

      if (friendAccepted) {
        this.friends[this.friends.findIndex(f => (f.senderId === senderId && f.recipientId === this.currentUserId)
          || (f.senderId === this.currentUserId && f.recipientId === senderId))] = friend;
      } else {
        this.friends = this.friends.filter(f => !(f.senderId === senderId && f.recipientId === this.currentUserId)
          || (f.senderId === this.currentUserId && f.recipientId === senderId));
      }

      this.listener.changeCurrentUsersChanged(true);
      this.friendService.decrementCurrentFriendInvitesCount();
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public deleteFriend(friendId: string) {
    if (confirm('Are you sure you want to remove friend?')) {
      this.friendService.deleteFriend(friendId).subscribe(() => {
        this.notifier.push('Friend deleted', 'info');
        this.friends = this.friends.filter(f => !(f.senderId === friendId && f.recipientId === this.currentUserId)
          || (f.senderId === this.currentUserId && f.recipientId === friendId));

        this.listener.changeCurrentUsersChanged(true);
        this.friendService.decrementCurrentFriendInvitesCount();
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public onFiltersChanged() {
    this.friendsRequest.pageNumber = 1;
    this.filterFriends(this.filtersForm.get('friendName').value);
  }

  public onScroll() {
    if (this.friends.length < this.pagination.totalItems) {
      this.friendsRequest.pageNumber++;
      this.filterFriends(this.filtersForm.get('friendName').value, true);
    }
  }

  private filterFriends(friendName?: string, onScroll = false) {
    this.friendsRequest.friendName = friendName;

    this.friendService.getFriends(this.friendsRequest).subscribe(response => {
      const friends = response.result.friends;
      this.friends = onScroll ? [...this.friends, ...friends] : friends;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      friendName: ['']
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.friends = data.friendsResponse.result.friends;
      this.pagination = data.friendsResponse.pagination;
    });

    this.currentUserId = this.authService.currentUser.id;

    this.listener.currentUsersChanged.subscribe(usersChanged => {
      if (usersChanged) {
        this.filterFriends();
      }
    });
  }
}
