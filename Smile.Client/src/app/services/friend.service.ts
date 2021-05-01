import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { FriendsRequest } from '../resolvers/requests/friends-request';
import { FriendsResponse } from '../resolvers/responses/friends-response';

@Injectable({
  providedIn: 'root'
})
export class FriendService {
  private readonly communityApiUrl = environment.apiUrl + 'community/';

  private friendInvitesCount = new BehaviorSubject<number>(0);
  currentFriendInvitesCount = this.friendInvitesCount.asObservable();

  constructor(private httpClient: HttpClient) { }

  public getFriends(friendsRequest: FriendsRequest) {
    const paginatedResult: PaginatedResult<FriendsResponse> = new PaginatedResult<FriendsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', friendsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', friendsRequest.pageSize.toString());

    if (friendsRequest.friendName) {
      httpParams = httpParams.append('friendName', friendsRequest.friendName);
    }

    return this.httpClient.get<FriendsResponse>(this.communityApiUrl + 'friends', { observe: 'response', params: httpParams })
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

  public invite(recipientId: string) {
    return this.httpClient.post(this.communityApiUrl + 'friend/invite', { recipientId }, { observe: 'response' });
  }

  public receive(senderId: string, recipientId: string, accepted: boolean) {
    return this.httpClient.put(this.communityApiUrl + 'friend/receive', { senderId, recipientId, accepted }, { observe: 'response' });
  }

  public deleteFriend(friendId: string) {
    return this.httpClient.delete(this.communityApiUrl + 'friend/delete', { params: { friendId } });
  }

  public countFriendInvites() {
    return this.httpClient.get<any>(this.communityApiUrl + 'friends/invites/count').subscribe(response => {
      this.changeCurrentFriendInvitesCount(response.friendInvitesCount);
    });
  }

  public changeCurrentFriendInvitesCount(friendInvitesCount: number) {
    this.friendInvitesCount.next(friendInvitesCount);
  }

  public decrementCurrentFriendInvitesCount() {
    if (this.friendInvitesCount.value > 0) {
      this.friendInvitesCount.next(this.friendInvitesCount.value - 1);
    }
  }
}
