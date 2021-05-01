import { AfterViewChecked, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Message } from 'src/app/models/domain/messenger/message';
import { Recipient } from 'src/app/models/domain/messenger/recipient';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { MessagesRequest } from 'src/app/resolvers/requests/messages-request';
import { AuthService } from 'src/app/services/auth.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-messages-thread',
  templateUrl: './messages-thread.component.html',
  styleUrls: ['./messages-thread.component.scss']
})
export class MessagesThreadComponent implements OnInit, OnDestroy, AfterViewChecked {
  @ViewChild('chatSection') chatSection: ElementRef;

  messagesThread: Message[];
  pagination: Pagination;
  recipient: Recipient;

  messageForm: FormGroup;

  messagesRequest = new MessagesRequest();

  constants = constants;

  isScrolled = false;
  private isMessengerOpened = true;

  constructor(private messenger: Messenger, private route: ActivatedRoute, public formHelper: FormHelper,
    private formBuilder: FormBuilder, private authService: AuthService, private notifier: Notifier,
    private signalr: Signalr, private router: Router) { }

  ngOnInit(): void {
    this.createMessageForm();
    this.subscribeData();
    this.subscribeSignalr();
  }

  ngOnDestroy() {
    this.decrementUnreadMessagesCount();
    this.isMessengerOpened = false;
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  public sendMessage() {
    if (this.messageForm.valid) {
      this.isScrolled = false;

      this.messenger.sendMessage(this.recipient.id, this.messageForm.value.text).subscribe(res => {
        const response: any = res?.body;
        this.messagesThread.push(response?.message);
        this.formHelper.resetForm(this.messageForm);
      }, error => {
        this.notifier.push(error, 'error');
      }, () => {
        this.scrollToBottom();
      });
    }
  }

  public onScroll() {
    if (this.messagesThread.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.loadMessagesThread(true);
      this.isScrolled = true;
    }
  }

  public onMessageDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.messagesThread = this.messagesThread.filter(m => m.id !== emitter.objectId);
    }
  }

  private loadMessagesThread(onScroll = false) {
    if (this.isMessengerOpened) {
      this.messagesRequest.pageNumber = onScroll ? this.pagination.currentPage : 1;

      this.messenger.getMessagesThread(this.messagesRequest).subscribe(response => {
        const messages = response.result.messages;

        this.messagesThread.reverse();
        this.messagesThread = onScroll ? this.messagesThread.concat(messages) : messages;
        this.messagesThread.reverse();

        this.pagination = response?.pagination;
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  private createMessageForm() {
    this.messageForm = this.formBuilder.group({
      text: ['', [Validators.required, Validators.maxLength(constants.messageLength)]]
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.messagesThread = data.messagesResponse.result.messages;
      this.pagination = data.messagesResponse.pagination;
      this.recipient = data.messagesResponse.result.recipient;
      this.messagesRequest.recipientId = this.recipient.id;
    });

    this.messenger.decrementCurrentUnreadMessagesCount();

    this.messagesThread.reverse();
  }

  private subscribeSignalr() {
    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_RECEIVED, values => {
      if (this.authService.isLoggedIn() && values[0].senderId === this.recipient.id) {
        this.messagesThread.push(values[0]);
      }

      if (this.router.url === `/messenger/${values[0].senderId}`) {
        this.messenger.readMessage(values[0].id).subscribe(() => {
          this.messenger.countUnreadMessages();
        }, () => { });
      } else {
        this.messenger.countUnreadMessages();
      }
    });

    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_DELETED, values => {
      if (this.authService.isLoggedIn()) {
        this.messagesThread = this.messagesThread.filter(m => m.id !== values[0]);
      }
    });
  }

  private decrementUnreadMessagesCount() {
    if (this.messagesThread.some(m => !m.isRead)) {
      this.messenger.decrementCurrentUnreadMessagesCount();
    }
  }

  private scrollToBottom() {
    if (this.chatSection && !this.isScrolled) {
      this.chatSection.nativeElement.scrollTop = this.chatSection.nativeElement.scrollHeight;
    }
  }
}
