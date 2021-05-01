import { Comment } from './comment';
import { Like } from './like';

export interface Post {
  id: string;
  title: string;
  content: string;
  dateCreated: Date;
  dateUpdated: Date;
  photoUrl: string;
  authorId: string;
  groupId: string;
  authorName: string;
  commentsCount: number;
  likesCount: number;

  comments: Comment[];
  likes: Like[];
}
