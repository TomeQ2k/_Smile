import { PaginationRequest } from './pagination-request';

export class MessagesRequest extends PaginationRequest {
  recipientId: string;
}
