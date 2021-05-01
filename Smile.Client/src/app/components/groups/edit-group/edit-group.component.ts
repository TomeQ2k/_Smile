import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Group } from 'src/app/models/domain/group/group';
import { GroupManager } from 'src/app/services/group-manager.service';
import { GroupService } from 'src/app/services/group.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.scss']
})
export class EditGroupComponent implements OnInit {
  @Input() group: Group;

  groupForm: FormGroup;

  image: File;
  changeImage = false;

  createMode: boolean;

  constants = constants;

  isPrivate: boolean;

  constructor(private groupManager: GroupManager, private groupService: GroupService, private route: ActivatedRoute,
              private formBuilder: FormBuilder, public formHelper: FormHelper, private notifier: Notifier,
              private router: Router) { }

  ngOnInit(): void {
    this.subscribeData();
    this.createGroupForm();
  }

  public createGroup() {
    if (this.groupForm.valid) {
      this.groupService.createGroup(this.groupForm.value, this.image).subscribe(() => {
        this.notifier.push('Group created', 'success');
        this.router.navigate(['groups']);
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/groups/create']);
      });
    }
  }

  public updateGroup() {
    if (this.groupForm.valid) {
      this.group = Object.assign(this.group, this.groupForm.value);

      this.groupManager.updateGroup(this.group.id, this.groupForm.value, this.changeImage, this.image).subscribe(res => {
        const response: any = res?.body;

        this.notifier.push('Group updated', 'success');
        this.groupForm.markAsPristine();
        this.group.imageUrl = response.group.imageUrl;
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['/group', this.group.id]);
      });
    }
  }

  public deleteGroup() {
    if (confirm('Are you sure you want to delete group with all contents and members?')) {
      this.groupManager.deleteGroup(this.group.id).subscribe(() => {
        this.notifier.push('Group has been deleted', 'info');
        this.router.navigate(['groups']);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public imageChanged = (image: File) => {
    this.image = image;
    this.changeImage = true;
    this.groupForm.markAsDirty();
  }

  public toggleIsPrivate() {
    this.isPrivate = !this.isPrivate;
  }

  private createGroupForm() {
    this.groupForm = this.formBuilder.group({
      name: [this.createMode ? '' : this.group.name, [Validators.required, Validators.maxLength(constants.titleLength)]],
      description: [this.createMode ? '' : this.group.description, [Validators.maxLength(constants.contentLength)]],
      isPrivate: [this.createMode ? false : this.group.isPrivate],
      joinCode: [this.createMode ? '' : this.group.joinCode, [Validators.maxLength(constants.groupCodeLength)]],
      inviteMemberPermission: [this.createMode ? 0 : this.group.inviteMemberPermission, [Validators.min(0), Validators.max(2)]],
      removeMemberPermission: [this.createMode ? 0 : this.group.removeMemberPermission, [Validators.min(0), Validators.max(1)]]
    });
  }

  private subscribeData = () => {
    this.route.queryParams.subscribe(params => {
      this.createMode = params.createMode;
    });

    if (this.group) {
      this.isPrivate = this.group.isPrivate;
    }
  }
}
