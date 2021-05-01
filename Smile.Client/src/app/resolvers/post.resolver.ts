import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { PostService } from '../services/post.service';
import { PostResponse } from './responses/post-response';

@Injectable()
export class PostResolver implements Resolve<PostResponse> {
  constructor(private router: Router, private postService: PostService,
              private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<PostResponse> {
    if (!route.queryParams.createMode) {
      return this.postService.getPost(route.params.postId).pipe(
        catchError(() => {
          this.notifier.push('Error occurred during loading data', 'error');
          this.router.navigate(['']);

          return of(null);
        }),
      );
    }

    return of(null);
  }
}
