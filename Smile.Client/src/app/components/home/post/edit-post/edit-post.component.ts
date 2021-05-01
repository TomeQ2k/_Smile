import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Post } from 'src/app/models/domain/main/post';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.scss']
})
export class EditPostComponent implements OnInit {
  post: Post;

  editMode: boolean;

  photo: File;
  changePhoto = false;

  postForm: FormGroup;

  constants = constants;

  groupId: string;

  constructor(private postService: PostService, private route: ActivatedRoute, private formBuilder: FormBuilder,
              private notifier: Notifier, private router: Router, public formHelper: FormHelper,
              private authService: AuthService) { }

  ngOnInit(): void {
    if (this.editMode && this.authService.currentUser?.id !== this.post?.authorId) {
      this.router.navigate(['']);
    }

    this.subscribeData();
    this.createPostForm();
  }

  public putPost() {
    if (this.postForm.valid) {
      const { title, content } = this.postForm.value;

      if (!this.editMode) {
        this.postService.createPost(title, content, this.photo, this.groupId).subscribe(() => {
          this.notifier.push('Post created', 'success');

          if (!this.groupId) {
            this.router.navigate(['']);
          } else {
            this.router.navigate(['/group', this.groupId]);
          }
        }, error => {
          this.notifier.push(error, 'error');
        });
      } else {
        this.postService.updatePost(this.post.id, title, content, this.photo, this.changePhoto).subscribe(() => {
          this.notifier.push('Post updated', 'success');
          this.router.navigate(['/posts', this.post.id]);
        }, error => {
          this.notifier.push(error, 'error');
        });
      }
    }
  }

  public photoChanged = (photo: File) => {
    this.photo = photo;
    this.changePhoto = true;
  }

  private createPostForm() {
    this.postForm = !this.editMode ? this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(constants.titleLength)]],
      content: ['', [Validators.required, Validators.maxLength(constants.contentLength)]]
    }) :
      this.formBuilder.group({
        title: [this.post?.title, [Validators.required, Validators.maxLength(constants.titleLength)]],
        content: [this.post?.content, [Validators.required, Validators.maxLength(constants.contentLength)]]
      });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.post = data.postResponse?.post;
      this.editMode = this.post ? true : false;
    });

    this.route.queryParams.subscribe(params => {
      if (params.groupId) {
        this.groupId = params.groupId;
      }
    });
  }
}
