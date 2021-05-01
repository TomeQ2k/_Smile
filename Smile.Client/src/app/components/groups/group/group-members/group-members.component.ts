import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Group } from 'src/app/models/domain/group/group';
import { GroupMember } from 'src/app/models/domain/group/group-member';
import { AuthService } from 'src/app/services/auth.service';
import { GroupManager } from 'src/app/services/group-manager.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-group-members',
  templateUrl: './group-members.component.html',
  styleUrls: ['./group-members.component.scss']
})
export class GroupMembersComponent implements OnInit {
  @Input() group: Group;

  members: GroupMember[];
  moderators: GroupMember[];

  inviteForm: FormGroup;

  currentUserId: string;

  userInvitePermission: number;
  userKickPermission: number;
  invitePermission: number;
  kickPermission: number;

  constructor(private groupManager: GroupManager, private authService: AuthService, private notifier: Notifier,
              private formBuilder: FormBuilder, public formHelper: FormHelper, private router: Router) { }

  ngOnInit(): void {
    this.createInviteForm();
    this.currentUserId = this.authService.currentUser.id;
    this.fetchDataFromGroup();
    this.fetchCurrentUserPermissions();
  }

  public inviteMember() {
    if (this.inviteForm.valid) {
      const username = this.inviteForm.value.username;
      this.groupManager.canInviteMember(username, this.group.id).subscribe(response => {
        const { canInvite, userId } = response;

        if (canInvite) {
          this.groupManager.inviteMember(this.group.id, userId).subscribe(res => {
            const invitedMember: any = res?.body;
            this.notifier.push('Invite sent', 'info');

            if (this.group.adminId === this.currentUserId) {
              this.members.push({ ...invitedMember.member, username });
            }

            this.formHelper.resetForm(this.inviteForm);
          }, error => {
            this.notifier.push(error, 'error');
            this.router.navigate(['/group', this.group.id]);
          });
        } else {
          this.notifier.push('This user is already member of this group', 'warning');
        }
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/group', this.group.id]);
      });
    }
  }

  public leaveGroup() {
    if (confirm('Are you sure you want to leave this group?')) {
      this.groupManager.leaveGroup(this.group.id).subscribe(() => {
        this.notifier.push('You left group', 'info');
        this.router.navigate(['groups']);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public acceptMember(userId: string, accept: boolean) {
    if (confirm(`Are you sure you want to ${accept ? 'accept' : 'deny'} this invite?`)) {
      this.groupManager.acceptMember(this.group.id, userId, accept).subscribe(() => {
        if (accept) {
          const memberIndex = this.members.findIndex(m => m.userId === userId);
          this.members[memberIndex].isAccepted = true;
          this.members[memberIndex].isJoining = false;
          this.notifier.push('Invite accepted', 'success');
        } else {
          this.members = this.members.filter(m => m.userId !== userId);
          this.notifier.push('Invite denied', 'info');
        }
      }, error => {
          this.notifier.push(error, 'error');
          this.router.navigate(['/group', this.group.id]);
      });
    }
  }

  public kickMember(userId: string) {
    if (confirm('Are you sure you want to kick this user from this group?')) {
      this.groupManager.kickMember(this.group.id, userId).subscribe(() => {
        if (this.moderators.some(m => m.userId === userId)) {
          this.moderators = this.moderators.filter(m => m.userId !== userId);
        } else {
          this.members = this.members.filter(m => m.userId !== userId);
        }
        this.notifier.push('User has been kicked from this group', 'info');
      }, error => {
          this.notifier.push(error, 'error');
          this.router.navigate(['/group', this.group.id]);
      });
    }
  }

  public setModerator(userId: string, isModerator: boolean) {
    this.groupManager.setModerator(this.group.id, userId, isModerator).subscribe(() => {
      if (isModerator) {
        const moderator: GroupMember = { ...this.members.find(m => m.userId === userId), isModerator: true };
        this.members = this.members.filter(m => m.userId !== userId);
        this.moderators.unshift(moderator);
        this.notifier.push('Moderator has been added', 'success');
      } else {
        const member: GroupMember = { ...this.moderators.find(m => m.userId === userId), isModerator: false };
        this.moderators = this.moderators.filter(m => m.userId !== userId);
        this.members.unshift(member);
        this.notifier.push('Moderator has been deleted', 'info');
      }
    }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/group', this.group.id]);
    });
  }

  private fetchDataFromGroup() {
    this.members = this.group.members;
    this.moderators = this.group.moderators;

    this.invitePermission = this.group.inviteMemberPermission;
    this.kickPermission = this.group.removeMemberPermission;
  }

  private createInviteForm() {
    this.inviteForm = this.formBuilder.group({
      username: ['']
    });
  }

  private fetchCurrentUserPermissions() {
    if (this.currentUserId === this.group.adminId) {
      this.userInvitePermission = 0;
      this.userKickPermission = 0;
    } else if (this.moderators.some(m => this.currentUserId === m.userId)) {
      this.userInvitePermission = 1;
      this.userKickPermission = 1;
    } else {
      this.userInvitePermission = 2;
      this.userKickPermission = 2;
    }
  }
}
