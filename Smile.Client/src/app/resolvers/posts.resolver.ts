import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PostService } from '../services/post.service';
import { PaginatedResult } from '../models/helpers/pagination';
import { PostsResponse } from './responses/posts-response';
import { AuthService } from '../services/auth.service';
import { PostsRequest } from './requests/posts-request';

@Injectable()
export class PostsResolver implements Resolve<PaginatedResult<PostsResponse>> {
  constructor(private router: Router, private postService: PostService,
              private notifier: Notifier, private authService: AuthService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<PostsResponse>> {
    const postRequest = new PostsRequest();

    if (route.data.isProfile) {
      postRequest.userId = this.authService.currentUser?.id;
    }

    return this.postService.getPosts(postRequest).pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
