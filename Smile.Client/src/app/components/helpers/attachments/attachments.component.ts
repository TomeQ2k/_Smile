import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-attachments',
  templateUrl: './attachments.component.html',
  styleUrls: ['./attachments.component.scss']
})
export class AttachmentsComponent {
  @Input() attachments: File[] = [];

  @Output() attachmentsChanged = new EventEmitter<File[]>();

  constants = constants;

  constructor(private notifier: Notifier) { }

  public onAttachmentsChange(files: File[]) {
    if (Object.values(files).some(f => f.size / 1024 / 1024 > constants.maxFileSize)) {
      this.notifier.push(`Maximum file size is ${constants.maxFileSize} MB`, 'warning');
    } else {
      if (this.attachments.length + files.length > constants.maxFilesCount) {
        this.notifier.push(`Maximum attachments count is ${constants.maxFilesCount}`, 'warning');
      } else {
        this.attachments = [...this.attachments, ...files];
        this.attachmentsChanged.emit(this.attachments);
      }
    }
  }

  public deleteAttachment(file: File) {
    this.attachments = Object.values(this.attachments).filter(a => a.name !== file.name);
    this.attachmentsChanged.emit(this.attachments);
  }
}
