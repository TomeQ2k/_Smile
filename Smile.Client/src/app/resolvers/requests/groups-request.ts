import { PaginationRequest } from './pagination-request';

export class GroupsRequest extends PaginationRequest {
  name: string;
  accessStatus: number;
  joinStatus: number;
  sortType: number;
  isInvited: boolean;
}
