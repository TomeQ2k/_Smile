import { UserProfile } from 'src/app/models/domain/profile/user-profile';
import { BaseResponse } from 'src/app/resolvers/responses/base-response';

export class ProfileResponse extends BaseResponse {
  userProfile: UserProfile;
}
