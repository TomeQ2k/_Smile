import { Post } from '../main/post';
import { GroupMember } from './group-member';

export interface Group {
  id: string;
  name: string;
  description: string;
  isPrivate: boolean;
  adminId: string;
  imageUrl: string;
  dateCreated: Date;
  adminName: string;
  adminPhotoUrl: string;
  membersCount: number;
  joinCode: string;
  isMember: boolean;
  isAccepted: boolean;
  inviteMemberPermission: number;
  removeMemberPermission: number;

  posts: Post[];
  members: GroupMember[];
  moderators: GroupMember[];
}
