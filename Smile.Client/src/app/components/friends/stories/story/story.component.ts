import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Direction } from 'src/app/enums/direction.enum';
import { Story } from 'src/app/models/domain/story/story';
import { StoryWrapper } from 'src/app/models/helpers/story/story-wrapper';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { StoryManager } from 'src/app/services/story-manager.service';
import { roles } from 'src/environments/environment';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.scss']
})
export class StoryComponent implements OnInit {
  @Input() storyWrapper: StoryWrapper;

  currentStory: Story;
  index = 0;

  direction = Direction;

  @Output() storyClosed = new EventEmitter<boolean>();

  constructor(private storyManager: StoryManager, private notifier: Notifier, private authService: AuthService) { }

  ngOnInit(): void {
    this.initFirstStory();
  }

  public changeIndex(direction: Direction) {
    this.index += direction === Direction.next
      ? (this.index + 1 >= this.storyWrapper.stories.length ? -this.index : 1)
      : (this.index - 1 < 0 ? -this.index : -1);
    this.currentStory = this.storyWrapper.stories[this.index];

    this.watchStory();
  }

  public deleteStory() {
    this.storyManager.deleteStory(this.currentStory.id).subscribe(() => {
      this.notifier.push('Story deleted', 'info');
      this.storyWrapper.stories = this.storyWrapper.stories.filter(s => s.id !== this.currentStory.id);

      if (this.storyWrapper.stories.length === 0) {
        this.storyClosed.emit(true);
      } else {
        this.index = 0;
        this.currentStory = this.storyWrapper.stories[this.index];
      }
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public isPermittedToDelete() {
    return this.currentStory.userId === this.authService.currentUser?.id || this.authService.checkPermissions(roles.adminRoles);
  }

  private watchStory() {
    if (!this.currentStory.isWatched) {
      this.storyManager.watchStory(this.currentStory.id).subscribe(() => {
        this.currentStory.isWatched = true;
        this.currentStory.watchedByCount++;
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  private initFirstStory() {
    this.currentStory = this.storyWrapper.stories[this.index];
    this.watchStory();
  }
}
