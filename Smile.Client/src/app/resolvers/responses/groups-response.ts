import { GroupList } from 'src/app/models/domain/group/group-list';
import { BaseResponse } from './base-response';

export class GroupsResponse extends BaseResponse {
  groups: GroupList[];
}
