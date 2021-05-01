import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  post: Post;

  commentForm: FormGroup;

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public onCommentPut(emitter: PutEmitter) {
    if (emitter.object) {
      if (!emitter.updated) {
        this.post.comments = [...this.post.comments, emitter.object];
        this.post.commentsCount++;
      } else {
        this.post.comments[this.post.comments.findIndex(c => c.id === emitter.object.id)] = emitter.object;
        this.post.comments = this.post.comments.sort((a, b) => new Date(a.dateUpdated).getTime() - new Date(b.dateUpdated).getTime());
      }
    }
  }

  public onCommentDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.post.comments = this.post.comments.filter(c => c.id !== emitter.objectId);
      this.post.commentsCount--;
    }
  }

  public onPostDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.router.navigate(['']);
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => this.post = data.postResponse.post);
  }
}
