import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserAuth } from '../models/domain/auth/user-auth';

@Injectable({
  providedIn: 'root'
})
export class ListenerService {
  private user = new BehaviorSubject<UserAuth>(null);
  currentUser = this.user.asObservable();

  private usersChanged = new BehaviorSubject<boolean>(false);
  currentUsersChanged = this.usersChanged.asObservable();

  public changeCurrentUser(user: UserAuth) {
    localStorage.setItem('user', JSON.stringify(user));
    this.user.next(user);
  }

  public changeCurrentUsersChanged(usersChanged: boolean) {
    this.usersChanged.next(usersChanged);
  }
}
