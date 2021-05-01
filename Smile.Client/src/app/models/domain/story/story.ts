export interface Story {
  id: string;
  dateCreated: Date;
  dateExpires: Date;
  storyUrl: string;
  userId: string;
  username: string;
  userPhotoUrl: string;
  isWatched: boolean;
  watchedByCount: number;
}
