import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Comment } from 'src/app/models/domain/main/comment';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants, roles } from 'src/environments/environment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {
  @Input() postId: string;
  @Input() comments: Comment[];

  @Output() commentPut = new EventEmitter<PutEmitter>();
  @Output() commentDeleted = new EventEmitter<DeleteEmitter>();

  commentForm: FormGroup;

  editMode: boolean;

  roles = roles;

  constructor(private commentService: CommentService, private formBuilder: FormBuilder, private notifier: Notifier,
              public formHelper: FormHelper, public authService: AuthService) { }

  ngOnInit(): void {
    this.createCommentForm();
  }

  public putComment() {
    if (this.commentForm.valid) {
      if (!this.editMode) {
        this.commentService.createComment(this.commentForm.value.content, this.postId).subscribe(res => {
          const response: any = res?.body;

          this.notifier.push('Comment created', 'success');
          this.commentPut.emit({ object: response?.comment, updated: false });
          this.formHelper.resetForm(this.commentForm);
        }, error => {
          this.notifier.push(error, 'error');
        });
      } else {
        this.commentService.updateComment(this.commentForm.value.content, this.commentForm.value.commentId, this.postId).subscribe(res => {
          const response: any = res?.body;

          this.notifier.push('Comment updated', 'success');
          this.commentPut.emit({ object: response?.comment, updated: true });
          this.toggleEditMode();
        }, error => {
          this.notifier.push(error, 'error');
        });
      }
    }
  }

  public deleteComment(commentId: string) {
    if (confirm('Are you sure you want to delete this comment?')) {
      this.commentService.deleteComment(commentId).subscribe(() => {
        this.notifier.push('Comment deleted', 'info');
        this.commentDeleted.emit({ deleted: true, objectId: commentId });
      }, error => {
          this.notifier.push(error, 'error');
      });
    }
  }

  public toggleEditMode(comment?: Comment) {
    this.editMode = !this.editMode;
    if (this.editMode) {
      this.commentForm.setValue({
        content: comment?.content,
        commentId: comment?.id
      });
    } else {
      this.formHelper.resetForm(this.commentForm);
    }
  }

  private createCommentForm() {
    this.commentForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(constants.commentLength)]],
      commentId: ['']
    });
  }
}
