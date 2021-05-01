export interface Friend {
  senderId: string;
  recipientId: string;
  senderName: string;
  senderPhotoUrl: string;
  recipientName: string;
  recipientPhotoUrl: string;
  senderAccepted: boolean;
  recipientAccepted: boolean;
  isAccepted: boolean;
}
