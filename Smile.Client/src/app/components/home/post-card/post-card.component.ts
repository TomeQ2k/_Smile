import { Component, Input, OnInit, Output, EventEmitter, OnChanges } from '@angular/core';
import { Post } from 'src/app/models/domain/main/post';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { Notifier } from 'src/app/services/notifier.service';
import { colors, roles } from 'src/environments/environment';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit, OnChanges {
  @Input() post: Post;

  @Output() postDeleted = new EventEmitter<DeleteEmitter>();

  smilesCounterColor = 'black';

  isLoggedIn: boolean;
  currentUserId: string;

  @Input() groupAdminId: string;

  constructor(private authService: AuthService, private postService: PostService, private notifier: Notifier) { }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    if (this.isLoggedIn) {
      this.currentUserId = this.authService.currentUser.id;
    }
  }

  ngOnChanges() {
    this.setSmilesCounterColor();
  }

  public deletePost() {
    if (confirm('Are you sure you want to delete this post?')) {
      this.postService.deletePost(this.post.id).subscribe(() => {
        this.notifier.push('Post deleted', 'info');
        this.postDeleted.emit({ objectId: this.post.id, deleted: true });
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public likePost() {
    if (this.authService.isLoggedIn()) {
      this.postService.likePost(this.post.id).subscribe(res => {
        const response: any = res.body;
        this.notifier.push(response?.likeCreated ? 'Post smiled' : 'Post unsmiled', response?.likeCreated ? 'success' : 'info');

        if (response?.likeCreated) {
          this.post.likes.push(response?.like);
          this.post.likesCount++;
        } else {
          this.post.likes = this.post?.likes?.filter(l => l.userId !== this.authService.currentUser.id);
          this.post.likesCount--;
        }

        this.setSmilesCounterColor();
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public isAdminMode = (authorId: string) =>
    this.authService.isLoggedIn() && (this.authService.currentUser.id === authorId || this.authService.checkPermissions(roles.adminRoles));

  public isGroupAdmin = () => this.authService.isLoggedIn() && this.authService.currentUser.id === this.groupAdminId;

  private setSmilesCounterColor() {
    if (this.authService.isLoggedIn()) {
      this.smilesCounterColor = this.post.likes.some(l => l.userId === this.authService.currentUser.id) ? colors.greenColor : 'black';
    }
  }
}
