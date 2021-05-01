import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConnectionManager {
  private readonly signalrApiUrl = environment.apiUrl + 'connection/';

  constructor(private httpClient: HttpClient) { }

  public startConnection(connectionId: string) {
    return this.httpClient.post(this.signalrApiUrl + 'start', { connectionId });
  }

  public closeConnection(connectionId: string) {
    return this.httpClient.delete(this.signalrApiUrl + 'close', { params: { connectionId } });
  }
}
