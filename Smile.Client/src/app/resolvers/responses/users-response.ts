import { User } from 'src/app/models/domain/user/user';
import { BaseResponse } from 'src/app/resolvers/responses/base-response';

export class UsersResponse extends BaseResponse {
  users: User[];
}
