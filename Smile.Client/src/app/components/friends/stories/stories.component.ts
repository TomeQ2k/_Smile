import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoryWrapper } from 'src/app/models/helpers/story/story-wrapper';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { StoryManager } from 'src/app/services/story-manager.service';
import { blurToggleAnimation, constants } from 'src/environments/environment';

@Component({
  selector: 'app-stories',
  templateUrl: './stories.component.html',
  styleUrls: ['./stories.component.scss'],
  animations: blurToggleAnimation
})
export class StoriesComponent implements OnInit {
  storyWrappers: StoryWrapper[];

  currentStoryWrapper: StoryWrapper;
  isStoryDisplayed: boolean;

  constructor(private storyManager: StoryManager, private route: ActivatedRoute, private notifier: Notifier,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public displayStory(storyWrapper: StoryWrapper) {
    this.currentStoryWrapper = storyWrapper;
    this.isStoryDisplayed = true;
  }

  public closeStory() {
    this.isStoryDisplayed = false;
    this.fetchStories();
  }

  public addStory(photo: File) {
    if (photo) {
      if (photo.size / 1024 / 1024 <= constants.maxFileSize) {
        this.storyManager.addStory(photo).subscribe(res => {
          const response: any = res?.body;
          if (this.storyWrappers.some(s => s.userId === this.authService.currentUser?.id)) {
            const index = this.storyWrappers.findIndex(s => s.userId === this.authService.currentUser?.id);
            this.storyWrappers[index] = {
              ...this.storyWrappers[index],
              isWatched: false,
              stories: [response.story, ...this.storyWrappers[index].stories]
            };
          } else {
            this.storyWrappers.unshift({
              userId: this.authService.currentUser?.id,
              username: this.authService.currentUser?.username,
              userPhotoUrl: this.authService.currentUser?.photoUrl,
              isWatched: false,
              stories: [response.story]
            });
          }
          this.notifier.push('Story added', 'success');
        }, error => {
          this.notifier.push(error, 'error');
        });
      } else {
        this.notifier.push(`Maximum file size is ${constants.maxFileSize} MB`, 'warning');
      }
    }
  }

  private fetchStories() {
    this.storyManager.fetchStories().subscribe(response => {
      this.storyWrappers = response?.stories;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => this.storyWrappers = data.storiesResponse.stories);
  }
}
