export interface GroupList {
  id: string;
  name: string;
  isPrivate: boolean;
  adminId: string;
  imageUrl: string;
  membersCount: number;
  isMember: boolean;
  isAccepted: boolean;
  joinRequested: boolean;
  isInvited: boolean;
  hasCode: boolean;
}
