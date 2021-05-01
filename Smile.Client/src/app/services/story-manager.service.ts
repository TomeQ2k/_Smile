import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StoriesResponse } from '../resolvers/responses/stories-response';

@Injectable({
  providedIn: 'root'
})
export class StoryManager {
  private readonly storyApiUrl = environment.apiUrl + 'story/';

  constructor(private httpClient: HttpClient) { }

  public fetchStories() {
    return this.httpClient.get<StoriesResponse>(this.storyApiUrl + 'fetch');
  }

  public addStory(photo: File) {
    const formData = new FormData();

    formData.append('photo', photo);

    return this.httpClient.post(this.storyApiUrl + 'add', formData, { observe: 'response' });
  }

  public watchStory(storyId: string) {
    return this.httpClient.post(this.storyApiUrl + 'watch', { storyId });
  }

  public deleteStory(storyId: string) {
    return this.httpClient.delete(this.storyApiUrl + 'delete', { params: { storyId } });
  }
}
