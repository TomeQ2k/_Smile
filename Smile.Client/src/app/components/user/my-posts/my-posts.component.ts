import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { PostsRequest } from 'src/app/resolvers/requests/posts-request';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-my-posts',
  templateUrl: './my-posts.component.html',
  styleUrls: ['./my-posts.component.scss']
})
export class MyPostsComponent implements OnInit {
  posts: Post[];
  pagination: Pagination;

  constructor(private postService: PostService, private route: ActivatedRoute, private authService: AuthService,
              private notifier: Notifier) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public async nextPage(page: PageEvent) {
    await this.loadUserPosts(page.pageIndex + 1);
  }

  public onPostDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.posts = this.posts.filter(p => p.id !== emitter.objectId);
    }
  }

  private loadUserPosts(pageNumber: number) {
    const postsRequest = new PostsRequest();

    postsRequest.pageNumber = pageNumber;
    postsRequest.userId = this.authService.currentUser.id;

    this.postService.getPosts(postsRequest).subscribe(response => {
      if (response) {
        this.posts = response.result.posts;
        this.pagination = response.pagination;
      }
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.posts = data.postsResponse.result.posts;
      this.pagination = data.postsResponse.pagination;
    });
  }
}
