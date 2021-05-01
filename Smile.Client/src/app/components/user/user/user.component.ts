import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/domain/user/user';
import { UserAdmin } from 'src/app/models/domain/user/user-admin';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { AdminService } from 'src/app/services/admin.service';
import { AuthService } from 'src/app/services/auth.service';
import { FriendService } from 'src/app/services/friend.service';
import { Notifier } from 'src/app/services/notifier.service';
import { adminRoleId, roles } from 'src/environments/environment';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user: User | UserAdmin;

  currentUserId: string;

  constructor(private adminService: AdminService, private authService: AuthService, private route: ActivatedRoute,
    private notifier: Notifier, private router: Router, private changeDetector: ChangeDetectorRef,
    private friendService: FriendService) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public changeRole() {
    if (!this.user.isAdmin) {
      this.adminService.admitRole(this.user.id, adminRoleId).subscribe(() => {
        this.notifier.push('Admin role admitted', 'info');
        this.user.isAdmin = true;
      }, error => {
        this.notifier.push(error, 'error');
      });
    } else {
      this.adminService.revokeRole(this.user.id, adminRoleId).subscribe(() => {
        this.notifier.push('Admin role revoked', 'info');
        this.user.isAdmin = false;
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/users/', this.user.id]);
      });
    }
  }

  public blockUser = () => {
    this.adminService.blockAccount(this.user.id).subscribe(() => {
      this.user.isBlocked = !this.user.isBlocked;
      this.notifier.push(this.user.isBlocked ? 'Account has been blocked' : 'Account has been unblocked',
        this.user.isBlocked ? 'warning' : 'info');
    }, error => {
      this.notifier.push(error, 'error');
      this.router.navigate(['/users/', this.user.id]);
    });
  }

  public deleteUser = () => {
    if (confirm(`Are you sure you want to delete ${this.user.username} account?`)) {
      this.adminService.deleteUser(this.user.id).subscribe(() => {
        this.notifier.push('User deleted', 'info');
        this.router.navigate(['']);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public confirmAccount = () => {
    this.adminService.confirmAccount(this.user.id).subscribe(() => {
      this.notifier.push('Account confirmed', 'info');
      this.user.emailConfirmed = true;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public onFriendInvited(emitter: PutEmitter) {
    if (emitter.updated) {
      this.user = { ...this.user, isFriend: true, isFriendAccepted: false, isCurrentUserSender: true };
    }
  }

  public onFriendAccepted(emitter: PutEmitter) {
    if (emitter.updated) {
      this.user = { ...this.user, isFriend: true, isFriendAccepted: true, isCurrentUserSender: true };
      this.friendService.decrementCurrentFriendInvitesCount();
    }
  }

  public onFriendDenied(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.user = { ...this.user, isFriend: false, isFriendAccepted: false, isCurrentUserSender: true };
      this.friendService.decrementCurrentFriendInvitesCount();
    }
  }

  public onFriendDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.user = { ...this.user, isFriend: false, isFriendAccepted: false, isCurrentUserSender: false };
      this.friendService.decrementCurrentFriendInvitesCount();
    }
  }

  public isAdmin = () => {
    return this.currentUserId !== this.user.id && this.authService.checkPermissions(roles.adminRoles);
  }

  public isHeadAdmin = () => {
    return this.authService.checkPermissions([roles.headAdminRole]);
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.user = data.userResponse.user;
      this.changeDetector.detectChanges();
    });
    this.currentUserId = this.authService.currentUser.id;
  }
}
