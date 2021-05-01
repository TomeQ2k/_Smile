import { PaginationRequest } from './pagination-request';

export class UsersRequest extends PaginationRequest {
  username: string;
  sortType = 0;
  onlyAdmin: boolean;

  emailConfirmedStatus: number;
  blockStatus: number;
}
