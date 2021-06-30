import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RoleName } from '../enums/role-name.enum';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly adminApiUrl = environment.apiUrl + 'admin/';

  constructor(private httpClient: HttpClient) { }

  public admitRole(role: RoleName, userId: string) {
    return this.httpClient.post(this.adminApiUrl + 'users/admitRole', { role, userId });
  }

  public revokeRole(role: RoleName, userId: string) {
    return this.httpClient.delete(this.adminApiUrl + 'users/revokeRole', { params: { role: role.toString(), userId } });
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
