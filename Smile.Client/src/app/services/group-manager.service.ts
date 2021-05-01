import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupManager {
  private readonly groupManagerApiUrl = environment.apiUrl + 'groupManager/';

  constructor(private httpClient: HttpClient) { }

  public inviteMember(groupId: string, userId: string) {
    return this.httpClient.post(this.groupManagerApiUrl + 'members/invite', { groupId, userId }, { observe: 'response' });
  }

  public acceptMember(groupId: string, userId: string, accept: boolean) {
    return this.httpClient.put(this.groupManagerApiUrl + 'members/accept', { groupId, userId, accept });
  }

  public kickMember(groupId: string, userId: string) {
    return this.httpClient.delete(this.groupManagerApiUrl + 'members/kick', { params: { groupId, userId } });
  }

  public leaveGroup(groupId: string) {
    return this.httpClient.delete(this.groupManagerApiUrl + 'members/leave', { params: { groupId } });
  }

  public updateGroup(groupId: string, request: any, changeImage: boolean, image?: File) {
    const formData = new FormData();

    formData.append('groupId', groupId);
    formData.append('name', request.name);

    if (request.description) {
      formData.append('description', request.description);
    }

    formData.append('isPrivate', request.isPrivate.toString());

    if (image && changeImage) {
      formData.append('image', image);
    }

    formData.append('changeImage', changeImage.toString());

    if (request.joinCode) {
      formData.append('joinCode', request.joinCode);
    }

    formData.append('inviteMemberPermission', request.inviteMemberPermission.toString());
    formData.append('removeMemberPermission', request.removeMemberPermission.toString());

    return this.httpClient.put(this.groupManagerApiUrl + 'update', formData, { observe: 'response' });
  }

  public deleteGroup(groupId: string) {
    return this.httpClient.delete(this.groupManagerApiUrl + 'delete', { params: { groupId } });
  }

  public setModerator(groupId: string, userId: string, isModerator: boolean) {
    return this.httpClient.patch(this.groupManagerApiUrl + 'members/setModerator', { groupId, userId, isModerator });
  }

  public canInviteMember(username: string, groupId: string) {
    return this.httpClient.get<any>(this.groupManagerApiUrl + 'members/canInvite', { params: { username, groupId } });
  }
}
