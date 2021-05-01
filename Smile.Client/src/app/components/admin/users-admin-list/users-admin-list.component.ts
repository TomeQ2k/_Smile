import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { SortType } from 'src/app/enums/sort-type.enum';
import { User } from 'src/app/models/domain/user/user';
import { Pagination } from 'src/app/models/helpers/pagination';
import { UsersRequest } from 'src/app/resolvers/requests/users-request';
import { Notifier } from 'src/app/services/notifier.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users-admin-list',
  templateUrl: './users-admin-list.component.html',
  styleUrls: ['./users-admin-list.component.scss']
})
export class UsersAdminListComponent implements OnInit {
  users: User[];
  pagination: Pagination;

  usersRequest = new UsersRequest();

  filtersForm: FormGroup;

  sortType = SortType;

  constructor(private userService: UserService, private route: ActivatedRoute, private formBuilder: FormBuilder,
              private notifier: Notifier) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();
  }

  public onFiltersChanged() {
    this.filterUsers();
  }

  public async nextPage(page: PageEvent) {
    await this.filterUsers(page.pageIndex + 1);
  }

  public resetFilters() {
    this.usersRequest = new UsersRequest();
    this.createFiltersForm();
    this.filterUsers();
  }

  private filterUsers(pageNumber = 1) {
    if (this.pagination.currentPage !== pageNumber) {
      this.usersRequest.pageNumber = pageNumber;
    }

    this.usersRequest = Object.assign(this.usersRequest, this.filtersForm.value);

    this.userService.getUsers(this.usersRequest).subscribe(response => {
      this.users = response.result.users;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      username: [''],
      sortType: [SortType.Descending],
      onlyAdmin: [false],
      emailConfirmedStatus: [0],
      blockStatus: [0]
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.users = data.usersResponse.result.users;
      this.pagination = data.usersResponse.pagination;
    });
  }
}
