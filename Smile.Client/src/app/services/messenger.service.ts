import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/helpers/pagination';
import { ConversationsRequest } from '../resolvers/requests/conversations-request';
import { MessagesRequest } from '../resolvers/requests/messages-request';
import { ConversationsResponse } from '../resolvers/responses/conversations-response';
import { MessagesResponse } from '../resolvers/responses/messages-response';

@Injectable({
  providedIn: 'root'
})
export class Messenger {
  private readonly messengerApiUrl = environment.apiUrl + 'messenger/';

  private unreadMessagesCount = new BehaviorSubject<number>(0);
  currentUnreadMessagesCount = this.unreadMessagesCount.asObservable();

  constructor(private httpClient: HttpClient) { }

  public getConversations(conversationsRequest: ConversationsRequest) {
    const paginatedResult: PaginatedResult<ConversationsResponse> = new PaginatedResult<ConversationsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', conversationsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', conversationsRequest.pageSize.toString());

    if (conversationsRequest.username) {
      httpParams = httpParams.append('username', conversationsRequest.username);
    }

    return this.httpClient.get<ConversationsResponse>(this.messengerApiUrl + 'conversations', { observe: 'response', params: httpParams })
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

  public getMessagesThread(messagesRequest: MessagesRequest) {
    const paginatedResult: PaginatedResult<MessagesResponse> = new PaginatedResult<MessagesResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', messagesRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', messagesRequest.pageSize.toString());
    httpParams = httpParams.append('recipientId', messagesRequest.recipientId);

    return this.httpClient.get<MessagesResponse>(this.messengerApiUrl + 'messages', { observe: 'response', params: httpParams })
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

  public sendMessage(recipientId: string, text: string) {
    return this.httpClient.post(this.messengerApiUrl + 'message/send', { recipientId, text }, { observe: 'response' });
  }

  public deleteMessage(messageId: string, recipientId?: string) {
    return this.httpClient.delete(this.messengerApiUrl + 'message/delete', { params: { messageId, recipientId } });
  }

  public deleteConversation(recipientId: string) {
    return this.httpClient.delete(this.messengerApiUrl + 'conversation/delete', { params: { recipientId } });
  }

  public readMessage(messageId: string) {
    return this.httpClient.patch(this.messengerApiUrl + 'message/read', { messageId });
  }

  public countUnreadMessages() {
    return this.httpClient.get<any>(this.messengerApiUrl + 'messages/unread/count').subscribe(response => {
      this.changeCurrentUnreadMessagesCount(response.unreadMessagesCount);
    });
  }

  public changeCurrentUnreadMessagesCount(unreadMessagesCount: number) {
    this.unreadMessagesCount.next(unreadMessagesCount);
  }

  public decrementCurrentUnreadMessagesCount() {
    if (this.unreadMessagesCount.value > 0) {
      this.unreadMessagesCount.next(this.unreadMessagesCount.value - 1);
    }
  }
}
