import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-image-card',
  templateUrl: './image-card.component.html',
  styleUrls: ['./image-card.component.scss']
})
export class ImageCardComponent {
  image: File;
  @Output() imageChanged = new EventEmitter<File>();

  @Input() imageUrl: string;

  constants = constants;

  constructor(private notifier: Notifier) { }

  public setImage(image: File) {
    if (image) {
      if (image.size / 1024 / 1024 <= constants.maxFileSize) {
        this.image = image;

        const reader = new FileReader();
        reader.readAsDataURL(image);

        reader.onload = () => {
          const url = reader.result.toString();
          this.imageUrl = url;

          this.imageChanged.emit(image);
        };
      } else {
        this.notifier.push(`Maximum file size is ${constants.maxFileSize} MB`, 'warning');
      }
    }
  }

  public resetImage() {
    this.image = null;
    this.imageUrl = null;
    this.imageChanged.emit(null);
  }
}
