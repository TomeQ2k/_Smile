import { Friend } from 'src/app/models/domain/community/friend';
import { BaseResponse } from 'src/app/resolvers/responses/base-response';

export class FriendsResponse extends BaseResponse {
  friends: Friend[];
}
