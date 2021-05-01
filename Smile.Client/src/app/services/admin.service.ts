import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly adminApiUrl = environment.apiUrl + 'admin/';

  constructor(private httpClient: HttpClient) { }

  public admitRole(userId: string, roleId: string) {
    return this.httpClient.post(this.adminApiUrl + 'users/admitRole', { userId, roleId });
  }

  public revokeRole(userId: string, roleId: string) {
    return this.httpClient.delete(this.adminApiUrl + 'users/revokeRole', { params: { userId, roleId } });
  }

  public deleteUser(userId: string) {
    return this.httpClient.delete(this.adminApiUrl + 'users/delete', { params: { userId } });
  }

  public blockAccount(userId: string) {
    return this.httpClient.patch(this.adminApiUrl + 'users/block', { userId });
  }

  public confirmAccount(userId: string) {
    return this.httpClient.patch(this.adminApiUrl + 'users/confirm', { userId });
  }
}
