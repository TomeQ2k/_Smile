import { Component, HostListener, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-host-listener',
  templateUrl: './host-listener.component.html',
  styleUrls: ['./host-listener.component.scss']
})
export class HostListenerComponent {
  @Input() form: FormGroup;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.form.dirty) {
      $event.returnValue = true;
    }
  }
}
