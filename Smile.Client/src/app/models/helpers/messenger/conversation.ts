import { LastMessage } from './last-message';

export interface Conversation {
  id: string;
  senderId: string;
  recipientId: string;
  username: string;
  avatarUrl: string;

  lastMessage: LastMessage;
}
