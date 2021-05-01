import { Group } from 'src/app/models/domain/group/group';
import { BaseResponse } from './base-response';

export class GroupResponse extends BaseResponse {
  group: Group;
}
