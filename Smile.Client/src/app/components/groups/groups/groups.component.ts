import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GroupList } from 'src/app/models/domain/group/group-list';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { GroupsRequest } from 'src/app/resolvers/requests/groups-request';
import { GroupService } from 'src/app/services/group.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  groups: GroupList[];
  pagination: Pagination;

  filtersForm: FormGroup;

  groupsRequest = new GroupsRequest();

  constructor(private groupService: GroupService, private route: ActivatedRoute, private formBuilder: FormBuilder,
              private notifier: Notifier) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();
  }

  public onScroll() {
    if (this.groups.length < this.pagination.totalItems) {
      this.groupsRequest.pageNumber++;
      this.fetchGroups(true);
    }
  }

  public onFiltersChanged() {
    this.groupsRequest.pageNumber = 1;
    this.groupsRequest = Object.assign(this.groupsRequest, this.filtersForm.value);
    this.fetchGroups();
  }

  public onGroupDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.groupsRequest = new GroupsRequest();
      this.createFiltersForm();
      this.groups = this.groups.filter(g => g.id !== emitter.objectId);
    }
  }

  public resetFilters() {
    this.groupsRequest = new GroupsRequest();
    this.createFiltersForm();
    this.fetchGroups();
  }

  private fetchGroups(onScroll = false) {
    this.groupService.fetchGroups(this.groupsRequest).subscribe(response => {
      const groups = response.result.groups;
      this.groups = onScroll ? [...this.groups, ...groups] : groups;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      name: [''],
      accessStatus: [0],
      joinStatus: [0],
      sortType: [0],
      isInvited: [false]
    });
  }

  private subscribeData = () => {
    this.route.data.subscribe(data => {
      this.groups = data.groupsResponse.result.groups;
      this.pagination = data.groupsResponse.pagination;
    });
  }
}
