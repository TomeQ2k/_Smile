export interface GroupMember {
  userId: string;
  groupId: string;
  isAccepted: boolean;
  isModerator: boolean;
  dateJoined: Date;
  username: string;
  userPhotoUrl: string;
  isJoining: boolean;
}
