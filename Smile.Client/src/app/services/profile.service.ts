import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProfileResponse } from '../resolvers/responses/profile-response';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private readonly profileApiUrl = environment.apiUrl + 'profile/';

  constructor(private httpClient: HttpClient) { }

  public getProfile(): Observable<ProfileResponse> {
    return this.httpClient.get<ProfileResponse>(this.profileApiUrl);
  }

  public changeUsername(request: any) {
    return this.httpClient.patch(this.profileApiUrl + 'changeUsername', request);
  }

  public changePassword(request: any) {
    return this.httpClient.put(this.profileApiUrl + 'changePassword', request);
  }

  public changeEmail(userId: string, token: string, newEmail: string) {
    return this.httpClient.get<any>(this.profileApiUrl + 'changeEmail', { params: { userId, token, newEmail } });
  }

  public sendChangeEmailCallback(request: any) {
    return this.httpClient.post(this.profileApiUrl + 'changeEmail/send', request);
  }

  public setAvatar(avatar: File) {
    const formData = new FormData();

    formData.append('avatar', avatar);

    return this.httpClient.post(this.profileApiUrl + 'avatar/set', formData);
  }

  public deleteAvatar() {
    return this.httpClient.delete(this.profileApiUrl + 'avatar/delete');
  }

  public refreshUserData() {
    return this.httpClient.get<any>(this.profileApiUrl + 'user/refresh');
  }
}
