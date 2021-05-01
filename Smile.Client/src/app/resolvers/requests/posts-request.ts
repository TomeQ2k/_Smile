import { PaginationRequest } from './pagination-request';

export class PostsRequest extends PaginationRequest {
  title: string;
  userId: string;
  sortType = 0;
  groupId: string;
}
