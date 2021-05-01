import { BaseResponse } from 'src/app/resolvers/responses/base-response';
import { Conversation } from 'src/app/models/helpers/messenger/conversation';

export class ConversationsResponse extends BaseResponse {
  conversations: Conversation[];
}
