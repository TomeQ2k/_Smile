import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Post } from '../models/domain/main/post';
import { PaginatedResult } from '../models/helpers/pagination';
import { PostsRequest } from '../resolvers/requests/posts-request';
import { PostsResponse } from '../resolvers/responses/posts-response';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private readonly postApiUrl = environment.apiUrl + 'post/';

  constructor(private httpClient: HttpClient) { }

  public getPost(postId: string) {
    return this.httpClient.get<Post>(this.postApiUrl, { params: { postId } });
  }

  public getPosts(postsRequest: PostsRequest) {
    const paginatedResult: PaginatedResult<PostsResponse> = new PaginatedResult<PostsResponse>();

    let httpParams = new HttpParams();

    httpParams = httpParams.append('pageNumber', postsRequest.pageNumber.toString());
    httpParams = httpParams.append('pageSize', postsRequest.pageSize.toString());

    if (postsRequest.title) {
      httpParams = httpParams.append('title', postsRequest.title);
    }

    if (postsRequest.groupId) {
      httpParams = httpParams.append('groupId', postsRequest.groupId);
    }

    if (postsRequest.userId) {
      httpParams = httpParams.append('userId', postsRequest.userId);
    }

    if (postsRequest.sortType) {
      httpParams = httpParams.append('sortType', postsRequest.sortType.toString());
    }

    return this.httpClient.get<PostsResponse>(this.postApiUrl + 'filter', { observe: 'response', params: httpParams })
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

  public createPost(title: string, content: string, photo: File, groupId?: string) {
    const formData = new FormData();

    formData.append('title', title);
    formData.append('content', content);

    if (photo) {
      formData.append('photo', photo, photo.name);
    }

    if (groupId) {
      formData.append('groupId', groupId);
    }

    return this.httpClient.post(this.postApiUrl + 'create', formData);
  }

  public updatePost(postId: string, title: string, content: string, photo: File, changePhoto: boolean) {
    const formData = new FormData();

    formData.append('postId', postId);
    formData.append('title', title);
    formData.append('content', content);

    if (photo && changePhoto) {
      formData.append('photo', photo, photo.name);
    }

    formData.append('changePhoto', changePhoto.toString());

    return this.httpClient.put(this.postApiUrl + 'update', formData);
  }

  public deletePost(postId: string) {
    return this.httpClient.delete(this.postApiUrl + 'delete', { params: { postId } });
  }

  public likePost(postId: string) {
    return this.httpClient.put(this.postApiUrl + 'like', { postId }, { observe: 'response' });
  }
}
