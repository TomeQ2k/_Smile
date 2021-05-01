import { UserRole } from './user-role';

export interface UserAuth {
  id: string;
  email: string;
  username: string;
  dateRegistered: Date;
  photoUrl: string;
  emailConfirmed: boolean;

  userRoles: UserRole[];
}
