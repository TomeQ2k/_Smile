import { Attachment } from './attachment';
import { Reply } from './reply';

export interface Report {
  id: string;
  dateCreated: Date;
  dateUpdated: Date;
  subject: string;
  content: string;
  reporterId: string;
  isClosed: string;
  reporterName: string;
  reporterPhotoUrl: string;
  email: string;
  isAnonymous: boolean;

  replies: Reply[];
  reportFiles: Attachment[];
}
