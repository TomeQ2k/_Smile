import { GroupList } from 'src/app/models/domain/group/group-list';
import { BaseResponse } from './base-response';

export class UserGroupsResponse extends BaseResponse {
  ownGroups: GroupList[];
  myGroups: GroupList[];
}
