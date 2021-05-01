import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/domain/user/user';
import { PaginatedResult } from '../models/helpers/pagination';
import { UsersRequest } from '../resolvers/requests/users-request';
import { UsersResponse } from '../resolvers/responses/users-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly userApiUrl = environment.apiUrl + 'user/';

  constructor(private httpClient: HttpClient) { }

  public getUser(userId: string) {
    return this.httpClient.get<User>(this.userApiUrl, { params: { userId } });
  }

  public getUsers(usersRequest: UsersRequest) {
    const paginatedResult: PaginatedResult<UsersResponse> = new PaginatedResult<UsersResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', usersRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', usersRequest.pageSize.toString());

    if (usersRequest.username) {
      httpParams = httpParams.append('username', usersRequest.username);
    }

    if (usersRequest.onlyAdmin) {
      httpParams = httpParams.append('onlyAdmin', usersRequest.onlyAdmin.toString());
    }

    if (usersRequest.emailConfirmedStatus) {
      httpParams = httpParams.append('emailConfirmedStatus', usersRequest.emailConfirmedStatus.toString());
    }

    if (usersRequest.blockStatus) {
      httpParams = httpParams.append('blockStatus', usersRequest.blockStatus.toString());
    }

    httpParams = httpParams.append('sortType', usersRequest.sortType.toString());

    return this.httpClient.get<UsersResponse>(this.userApiUrl + 'filter', { observe: 'response', params: httpParams })
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
}
