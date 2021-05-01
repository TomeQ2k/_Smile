import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { Notifier } from 'src/app/services/notifier.service';
import { SupportService } from 'src/app/services/support.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-create-reply',
  templateUrl: './create-reply.component.html',
  styleUrls: ['./create-reply.component.scss']
})
export class CreateReplyComponent implements OnInit {
  replyForm: FormGroup;

  attachments: File[] = [];

  @Input() reportId: string;

  @Output() replySent = new EventEmitter<PutEmitter>();

  constants = constants;

  constructor(private supportService: SupportService, private formBuilder: FormBuilder, public formHelper: FormHelper,
              private notifier: Notifier, private router: Router) { }

  ngOnInit(): void {
    this.createReplyForm();
  }

  public sendReply() {
    if (this.replyForm.valid) {
      this.supportService.sendReply(this.replyForm.value.content, this.reportId, this.attachments).subscribe(res => {
        const response: any = res?.body;

        this.notifier.push('Reply has been sent', 'success');
        this.replySent.emit({ updated: false, object: response.reply });
      }, error => {
          this.notifier.push(error, 'error');
          this.router.navigate(['/support/', this.reportId]);
      }, () => {
        this.formHelper.resetForm(this.replyForm);
        this.attachments = [];
      });
    }
  }

  public onChangeAttachments(files: File[]) {
    this.attachments = files;
  }

  private createReplyForm() {
    this.replyForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(constants.contentLength)]]
    });
  }
}
