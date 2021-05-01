import { Story } from '../../domain/story/story';

export interface StoryWrapper {
  userId: string;
  username: string;
  userPhotoUrl: string;
  isWatched: boolean;

  stories: Story[];
}
