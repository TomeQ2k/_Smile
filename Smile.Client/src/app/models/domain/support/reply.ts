import { Attachment } from './attachment';

export interface Reply {
  id: string;
  dateSent: Date;
  content: string;
  reportId: string;
  isAdmin: boolean;
  reporterName: string;

  replyFiles: Attachment[];
}
