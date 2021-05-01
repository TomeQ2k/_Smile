import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { GroupList } from 'src/app/models/domain/group/group-list';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { GroupManager } from 'src/app/services/group-manager.service';
import { GroupService } from 'src/app/services/group.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants, roles } from 'src/environments/environment';

@Component({
  selector: 'app-group-card',
  templateUrl: './group-card.component.html',
  styleUrls: ['./group-card.component.scss']
})
export class GroupCardComponent implements OnInit {
  @Input() group: GroupList;

  @Output() groupJoined = new EventEmitter<PutEmitter>();
  @Output() groupDeleted = new EventEmitter<DeleteEmitter>();

  joinForm: FormGroup;

  currentUserId: string;
  enterCodeMode = false;

  groupCodeLength = constants.groupCodeLength;
  adminRoles = roles.adminRoles;

  constructor(private groupService: GroupService, private groupManager: GroupManager, public authService: AuthService,
              private notifier: Notifier, private formBuilder: FormBuilder, public formHelper: FormHelper,
              private router: Router) { }

  ngOnInit() {
    this.createJoinForm();
    this.currentUserId = this.authService.currentUser?.id;
  }

  public joinGroup() {
    if (!this.group.isPrivate) {
      this.groupService.joinGroup(this.group.id).subscribe(() => {
        this.notifier.push('You joined this group', 'info');
        this.group.membersCount++;
        this.group.isMember = true;
      }, error => {
        this.notifier.push(error, 'error');
      });
    } else if (this.group.isPrivate && this.group.hasCode) {
      this.enterCodeMode = true;
    } else {
      this.groupService.joinGroup(this.group.id).subscribe(() => {
        this.notifier.push('You sent join request to this group', 'info');
        this.group.joinRequested = true;
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public enterCode() {
    if (this.enterCodeMode && this.group.isPrivate) {
      this.groupService.joinGroup(this.group.id, this.joinForm.get('joinCode').value).subscribe(() => {
        this.notifier.push('You joined this group', 'info');
        this.group.isMember = true;
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['groups']);
      }, () => {
          this.formHelper.resetForm(this.joinForm);
          this.enterCodeMode = false;
        });
      }
    }

    public acceptInvite(accept: boolean) {
      this.groupManager.acceptMember(this.group.id, this.currentUserId, accept).subscribe(() => {
        this.notifier.push(accept ? 'You joined this group' : 'You denied invite to this group', 'info');
        this.group = { ...this.group, isMember: accept, isInvited: false, joinRequested: false };
        if (accept) {
          this.group.membersCount++;
        }
      }, error => {
        this.notifier.push(error, 'error');
      });
    }

    public deleteGroup() {
      if (confirm('Are you sure you want to delete group with all contents and members?')) {
        this.groupManager.deleteGroup(this.group.id).subscribe(() => {
          this.notifier.push('Group has been deleted', 'info');
          this.groupDeleted.emit({ deleted: true, objectId: this.group.id });
        }, error => {
          this.notifier.push(error, 'error');
        });
      }
    }

  public canJoin = () => !this.group.joinRequested && !this.group.isInvited && this.currentUserId !== this.group.adminId
    && !this.group.isMember;

  public canAccept = () => !this.group.joinRequested && this.group.isInvited && this.currentUserId !== this.group.adminId;

  private createJoinForm() {
    this.joinForm = this.formBuilder.group({
      joinCode: ['', [Validators.required, Validators.maxLength(this.groupCodeLength)]]
    });
  }
}
