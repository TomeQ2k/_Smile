import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from 'src/app/models/domain/user/user';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { FriendService } from 'src/app/services/friend.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-friend-buttons',
  templateUrl: './friend-buttons.component.html',
  styleUrls: ['./friend-buttons.component.scss']
})
export class FriendButtonsComponent implements OnInit {
  @Input() user: User;

  @Output() friendInvited = new EventEmitter<PutEmitter>();
  @Output() friendAccepted = new EventEmitter<PutEmitter>();
  @Output() friendDenied = new EventEmitter<DeleteEmitter>();
  @Output() friendDeleted = new EventEmitter<DeleteEmitter>();

  currentUserId: string;

  constructor(private friendService: FriendService, private listener: ListenerService, private notifier: Notifier,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser.id;
  }

  public inviteFriend(recipientId: string) {
    this.friendService.invite(recipientId).subscribe(() => {
      this.notifier.push('Invite sent', 'info');

      this.friendInvited.emit({ updated: true, object: recipientId });

      this.listener.changeCurrentUsersChanged(true);
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public receiveFriend(senderId: string, recipientId: string, accepted: boolean) {
    this.friendService.receive(senderId, recipientId, accepted).subscribe(res => {
      const response: any = res?.body;
      const { friendAccepted } = response;

      this.notifier.push(`Invite ${(friendAccepted ? 'accepted' : 'ignored')}`, friendAccepted ? 'success' : 'info');

      if (friendAccepted) {
        this.friendAccepted.emit({ updated: true, object: senderId });
      } else {
        this.friendDenied.emit({ deleted: true, objectId: senderId });
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

        this.friendDeleted.emit({ deleted: true, objectId: friendId });

        this.listener.changeCurrentUsersChanged(true);
        this.friendService.decrementCurrentFriendInvitesCount();
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }
}
