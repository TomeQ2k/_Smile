import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GroupList } from 'src/app/models/domain/group/group-list';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';

@Component({
  selector: 'app-user-groups',
  templateUrl: './user-groups.component.html',
  styleUrls: ['./user-groups.component.scss']
})
export class UserGroupsComponent implements OnInit {
  @Input() groups: GroupList[];
  ownGroups: GroupList[];
  myGroups: GroupList[];

  @Input() isProfile = false;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public onGroupDeleted = (emitter: DeleteEmitter) => {
    this.ownGroups = this.ownGroups.filter(g => g.id !== emitter.objectId);
  }

  private subscribeData = () => {
    if (this.isProfile) {
      this.route.data.subscribe(data => {
        this.ownGroups = data.userGroupsResponse.ownGroups;
        this.myGroups = data.userGroupsResponse.myGroups;
      });
    }
  }
}
