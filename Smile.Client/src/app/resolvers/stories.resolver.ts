import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { StoriesResponse } from './responses/stories-response';
import { StoryManager } from '../services/story-manager.service';

@Injectable()
export class StoriesResolver implements Resolve<StoriesResponse> {
  constructor(private router: Router, private storyManager: StoryManager, private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<StoriesResponse> {
    return this.storyManager.fetchStories().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
