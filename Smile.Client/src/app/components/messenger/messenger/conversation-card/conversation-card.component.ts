import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Conversation } from 'src/app/models/helpers/messenger/conversation';
import { AuthService } from 'src/app/services/auth.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';
import { colors } from 'src/environments/environment';

@Component({
  selector: 'app-conversation-card',
  templateUrl: './conversation-card.component.html',
  styleUrls: ['./conversation-card.component.scss']
})
export class ConversationCardComponent implements OnInit {
  @Input() conversation: Conversation;

  @Output() conversationDeleted = new EventEmitter<DeleteEmitter>();

  currentUserId: string;

  colors = colors;

  constructor(private messenger: Messenger, private authService: AuthService, private notifier: Notifier) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser?.id;
  }

  public deleteConversation(recipientId: string) {
    if (confirm('Are you sure you want to delete this conversation?')) {
      this.messenger.deleteConversation(recipientId).subscribe(() => {
        this.conversationDeleted.emit({ deleted: true });
        this.notifier.push('Conversation deleted', 'info');
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }
}
