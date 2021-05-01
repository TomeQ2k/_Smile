import { Component, Input } from '@angular/core';
import { Reply } from 'src/app/models/domain/support/reply';

@Component({
  selector: 'app-replies',
  templateUrl: './replies.component.html',
  styleUrls: ['./replies.component.scss']
})
export class RepliesComponent {
  @Input() replies: Reply[];

  @Input() reporterPhotoUrl: string;
}
