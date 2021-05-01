import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Notifier } from '../services/notifier.service';
import { ProfileService } from '../services/profile.service';
import { ProfileResponse } from './responses/profile-response';

@Injectable()
export class ProfileResolver implements Resolve<ProfileResponse> {
  constructor(private router: Router, private profileService: ProfileService,
              private notifier: Notifier) { }

  resolve(route: ActivatedRouteSnapshot): Observable<ProfileResponse> {
    return this.profileService.getProfile().pipe(
      catchError(() => {
        this.notifier.push('Error occurred during loading data', 'error');
        this.router.navigate(['']);

        return of(null);
      }),
    );
  }
}
