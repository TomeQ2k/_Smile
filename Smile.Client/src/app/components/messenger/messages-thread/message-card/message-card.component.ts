import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Message } from 'src/app/models/domain/messenger/message';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-message-card',
  templateUrl: './message-card.component.html',
  styleUrls: ['./message-card.component.scss']
})
export class MessageCardComponent {
  @Input() message: Message;

  @Output() messageDeleted = new EventEmitter<DeleteEmitter>();

  constructor(private messenger: Messenger, private notifier: Notifier, public authService: AuthService) { }

  public deleteMessage(messageId: string) {
    if (confirm('Are you sure you want to delete this message?')) {
      this.messenger.deleteMessage(messageId,
        this.message.senderId === this.authService.currentUser.id ? this.message.recipientId : this.message.senderId)
        .subscribe(() => {
          this.notifier.push('Message deleted', 'info');
          this.messageDeleted.emit({ deleted: true, objectId: messageId });
        }, error => {
          this.notifier.push(error, 'error');
        });
    }
  }
}
