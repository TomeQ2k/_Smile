import { Message } from 'src/app/models/domain/messenger/message';
import { BaseResponse } from 'src/app/resolvers/responses/base-response';
import { Recipient } from 'src/app/models/domain/messenger/recipient';

export class MessagesResponse extends BaseResponse {
  messages: Message[];
  recipient: Recipient;
}
