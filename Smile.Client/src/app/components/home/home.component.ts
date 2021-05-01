import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { PostsRequest } from 'src/app/resolvers/requests/posts-request';
import { PostService } from 'src/app/services/post.service';
import { Notifier } from 'src/app/services/notifier.service';
import { pageSize } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  posts: Post[];
  pagination: Pagination;

  @Input() groupId: string;
  @Input() groupPosts: Post[];
  @Input() isMember: boolean;
  @Input() groupAdminId: string;

  postsRequest: PostsRequest = new PostsRequest();

  filtersForm: FormGroup;

  constructor(private postService: PostService, private notifier: Notifier, private route: ActivatedRoute, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();
  }

  public filterPosts() {
    this.postsRequest.pageNumber = 1;
    this.loadPosts();
  }

  public clearFilters() {
    this.postsRequest = new PostsRequest();

    if (this.groupId) {
      this.postsRequest.groupId = this.groupId;
    }

    this.createFiltersForm();
    this.loadPosts();
  }

  public onPostDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.posts = this.posts.filter(p => p.id !== emitter.objectId);
    }
  }

  public onScroll() {
    if (this.posts.length < this.pagination.totalItems) {
      this.postsRequest.pageNumber++;
      this.loadPosts(true);
    }
  }

  private loadPosts(onScroll = false) {
    this.postsRequest = Object.assign(this.postsRequest, this.filtersForm.value);

    this.postService.getPosts(this.postsRequest).subscribe(response => {
      const posts = response.result.posts;
      this.posts = onScroll ? [...this.posts, ...posts] : posts;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private subscribeData() {
    this.postsRequest.groupId = this.groupId;

    if (this.groupPosts) {
      this.posts = this.groupPosts;
      this.pagination = {
        currentPage: 1,
        itemsPerPage: pageSize,
        totalItems: this.posts.length,
        totalPages: Math.ceil(this.posts.length / pageSize)
      };
    } else {
      this.route.data.subscribe(data => {
        this.posts = data.postsResponse.result.posts;
        this.pagination = data.postsResponse.pagination;
      });
    }
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      title: '',
      sortType: 0
    });
  }
}
