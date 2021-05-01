import { GroupList } from '../group/group-list';
import { Post } from '../main/post';

export interface User {
  id: string;
  username: string;
  dateRegistered: Date;
  photoUrl: string;
  emailConfirmed: boolean;
  isBlocked: boolean;
  isFriend: boolean;
  isFriendAccepted: boolean;
  isCurrentUserSender: boolean;
  isAdmin: boolean;

  posts: Post[];
  groups: GroupList[];
}
