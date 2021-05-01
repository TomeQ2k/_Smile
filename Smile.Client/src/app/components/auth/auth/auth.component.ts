import { Component } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {
  resetPasswordMode = false;

  constructor() { }

  public resetPasswordToggle(resetPasswordMode: boolean) {
    this.resetPasswordMode = resetPasswordMode;
  }
}
