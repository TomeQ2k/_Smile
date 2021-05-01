import { BaseResponse } from 'src/app/resolvers/responses/base-response';
import { StoryWrapper } from 'src/app/models/helpers/story/story-wrapper';

export class StoriesResponse extends BaseResponse {
  stories: StoryWrapper[];
}
