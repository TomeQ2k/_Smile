import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ConnectionManager } from './connection-manager.service';

import * as signalr from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class Signalr {
  private static connectionId: string;

  private readonly hubApiUrl = environment.signalRUrl;
  private hubConnection: signalr.HubConnection;

  constructor(private connectionManager: ConnectionManager) { }

  public startConnection = () => {
    if (!this.hubConnection) {
      this.hubConnection = new signalr.HubConnectionBuilder()
        .withUrl(this.hubApiUrl)
        .build();

      this.hubConnection.start()
        .then(() => {
          this.initConnection();
          if (!environment.production) {
            console.log('SignalR: Connection started...');
          }
        })
        .catch(error => console.error('SignalR: ', error));
    }
  }

  public subscribeAction = (actionName: string, action: (value?: any) => void) => {
    if (this.hubConnection) {
      this.hubConnection.off(actionName);
      this.hubConnection.on(actionName, action);
    }
  }

  public closeConnection = () => {
    this.hubConnection.stop();
    this.hubConnection = null;

    if (!environment.production) {
      console.log('SignalR: Connection closed...');
    }
  }

  private initConnection = () => {
    this.hubConnection.invoke(SIGNALR_ACTIONS.GET_CONNECTION_ID)
      .then((connectionId) => {
        Signalr.connectionId = connectionId;
        this.createConnection();
      });
  }

  private createConnection = () => {
    this.connectionManager.startConnection(Signalr.connectionId).subscribe(() => {
      if (!environment.production) {
        console.log('SignalR: Connection established and persisted in database...');
      }
    });
  }
}

export const SIGNALR_ACTIONS = {
  ON_MESSAGE_RECEIVED: 'OnMessageReceived',
  ON_MESSAGE_DELETED: 'OnMessageDeleted',
  ON_FRIEND_INVITED: 'OnFriendInvited',
  ON_FRIEND_DELETED: 'OnFriendDeleted',
  ON_FRIEND_RECEIVED: 'OnFriendReceived',
  ON_NOTIFICATION_SENT: 'OnNotificationSent',
  REFRESH_USER_DATA: 'OnRefreshUserData',
  GET_CONNECTION_ID: 'GetConnectionId'
};
