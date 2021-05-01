import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Group } from '../models/domain/group/group';
import { PaginatedResult } from '../models/helpers/pagination';
import { GroupsRequest } from '../resolvers/requests/groups-request';
import { GroupsResponse } from '../resolvers/responses/groups-response';
import { UserGroupsResponse } from '../resolvers/responses/user-groups-response';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private readonly groupApiUrl = environment.apiUrl + 'group/';

  constructor(private httpClient: HttpClient) { }

  public getGroup(groupId: string) {
    return this.httpClient.get<Group>(this.groupApiUrl, { params: { groupId } });
  }

  public fetchGroups(groupsRequest: GroupsRequest) {
    const paginatedResult: PaginatedResult<GroupsResponse> = new PaginatedResult<GroupsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', groupsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', groupsRequest.pageSize.toString());

    if (groupsRequest.name) {
      httpParams = httpParams.append('name', groupsRequest.name);
    }

    if (groupsRequest.accessStatus) {
      httpParams = httpParams.append('accessStatus', groupsRequest.accessStatus.toString());
    }

    if (groupsRequest.joinStatus) {
      httpParams = httpParams.append('joinStatus', groupsRequest.joinStatus.toString());
    }

    if (groupsRequest.sortType) {
      httpParams = httpParams.append('sortType', groupsRequest.sortType.toString());
    }

    if (groupsRequest.isInvited) {
      httpParams = httpParams.append('isInvited', groupsRequest.isInvited.toString());
    }

    return this.httpClient.get<GroupsResponse>(this.groupApiUrl + 'filter', { observe: 'response', params: httpParams })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return paginatedResult;
        })
      );
  }

  public createGroup(request: any, image?: File) {
    const formData = new FormData();

    formData.append('name', request.name);

    if (request.description) {
      formData.append('description', request.description);
    }

    formData.append('isPrivate', request.isPrivate.toString());

    if (image) {
      formData.append('image', image);
    }

    if (request.joinCode) {
      formData.append('joinCode', request.joinCode);
    }

    formData.append('inviteMemberPermission', request.inviteMemberPermission.toString());
    formData.append('removeMemberPermission', request.removeMemberPermission.toString());

    return this.httpClient.post(this.groupApiUrl + 'create', formData, { observe: 'response' });
  }

  public joinGroup(groupId: string, joinCode?: string) {
    return this.httpClient.post(this.groupApiUrl + 'join', { groupId, joinCode });
  }

  public fetchUserGroups() {
    return this.httpClient.get<UserGroupsResponse>(this.groupApiUrl + 'user/groups');
  }
}
