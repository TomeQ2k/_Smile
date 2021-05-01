export interface ReportList {
  id: string;
  dateCreated: Date;
  dateUpdated: Date;
  subject: string;
  isClosed: boolean;
  reporterName: string;
  email: string;
  isAnonymous: boolean;
}
