import { Post } from 'src/app/models/domain/main/post';
import { BaseResponse } from 'src/app/resolvers/responses/base-response';

export class PostResponse extends BaseResponse {
  post: Post;
}
