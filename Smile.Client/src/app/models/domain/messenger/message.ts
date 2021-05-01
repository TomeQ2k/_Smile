export interface Message {
  id: string;
  senderId: string;
  recipientId: string;
  text: string;
  dateSent: Date;
  senderName: string;
  recipientName: string;
  senderPhotoUrl: string;
  recipientPhotoUrl: string;
  isRead: boolean;
}
