import { Component, Input, OnInit } from '@angular/core';
import { Attachment } from 'src/app/models/domain/support/attachment';

@Component({
  selector: 'app-attachments-list',
  templateUrl: './attachments-list.component.html',
  styleUrls: ['./attachments-list.component.scss']
})
export class AttachmentsListComponent implements OnInit {
  attachments: AttachmentModel[] = [];

  @Input() files: Attachment[];

  ngOnInit(): void {
    for (let i = 1; i <= this.files.length; i++) {
      this.attachments.push({ name: `Attachment #${i}`, url: this.files[i - 1].url });
    }
  }
}

interface AttachmentModel {
  name: string;
  url: string;
}
