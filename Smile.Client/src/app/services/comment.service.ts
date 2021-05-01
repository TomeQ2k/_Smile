import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private readonly commentApiUrl = environment.apiUrl + 'comment/';

  constructor(private httpClient: HttpClient) { }

  public createComment(content: string, postId: string) {
    return this.httpClient.post(this.commentApiUrl + 'create', { content, postId }, { observe: 'response' });
  }

  public updateComment(content: string, commentId: string, postId: string) {
    return this.httpClient.put(this.commentApiUrl + 'update', { content, commentId, postId }, { observe: 'response' });
  }

  public deleteComment(commentId: string) {
    return this.httpClient.delete(this.commentApiUrl + 'delete', { params: { commentId } });
  }
}
