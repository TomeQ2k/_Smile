import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Conversation } from 'src/app/models/helpers/messenger/conversation';
import { Pagination } from 'src/app/models/helpers/pagination';
import { ConversationsRequest } from 'src/app/resolvers/requests/conversations-request';
import { AuthService } from 'src/app/services/auth.service';
import { Messenger } from 'src/app/services/messenger.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr, SIGNALR_ACTIONS } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.scss']
})
export class MessengerComponent implements OnInit {
  conversations: Conversation[];
  pagination: Pagination;

  conversationsRequest = new ConversationsRequest();

  filtersForm: FormGroup;

  constructor(private messenger: Messenger, private route: ActivatedRoute, private notifier: Notifier,
    private signalr: Signalr, private formBuilder: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();

    this.signalr.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_RECEIVED, () => {
      if (this.authService.isLoggedIn()) {
        this.loadConversations();
        this.messenger.countUnreadMessages();
      }
    });
  }

  public onDeleteConversation(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.loadConversations();
    }
  }

  public onFiltersChanged() {
    this.conversationsRequest.pageNumber = 1;
    this.conversationsRequest.username = this.filtersForm.get('username').value;
    this.loadConversations();
  }

  public onScroll() {
    if (this.conversations.length < this.pagination.totalItems) {
      this.conversationsRequest.pageNumber++;
      this.loadConversations(true);
    }
  }

  private loadConversations(onScroll = false) {
    this.messenger.getConversations(this.conversationsRequest).subscribe(response => {
      const conversations = response.result.conversations;
      this.conversations = onScroll ? [...this.conversations, ...conversations] : conversations;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      username: ['']
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.conversations = data.conversationsResponse.result.conversations;
      this.pagination = data.conversationsResponse.pagination;
    });
  }
}
