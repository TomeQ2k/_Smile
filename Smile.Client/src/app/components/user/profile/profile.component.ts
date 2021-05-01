import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Signalr } from 'src/app/services/signalr.service';
import { roles } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  roles = roles;

  constructor(private authService: AuthService, private notifier: Notifier, private signalr: Signalr) { }

  ngOnInit(): void {
  }

  public logout() {
    this.authService.logout();
    this.notifier.push('Logged out', 'info');

    this.signalr.closeConnection();
  }
}
