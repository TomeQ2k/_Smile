import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FriendService } from 'src/app/services/friend.service';
import { Messenger } from 'src/app/services/messenger.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  @Output() resetPasswordToggled = new EventEmitter<boolean>();

  private returnToUrl: string;

  constructor(private authService: AuthService, private notifier: Notifier, private formBuilder: FormBuilder, private router: Router,
    private route: ActivatedRoute, private messenger: Messenger, private friendService: FriendService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.createLoginForm();

    this.returnToUrl = this.route.snapshot.queryParams.returnTo || '/';
  }

  public login() {
    const userLogin = Object.assign({}, this.loginForm.value);

    this.authService.login(userLogin).subscribe(() => {
      this.notifier.push('Logged in', 'success');
      this.router.navigate([this.returnToUrl]);
    }, error => {
      this.notifier.push(error, 'error');
      this.router.navigate(['/auth']);
    }, () => this.countUnread());
  }

  private createLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: ['']
    });
  }

  private countUnread() {
    this.messenger.countUnreadMessages();
    this.notificationService.countUnreadNotifications();
    this.friendService.countFriendInvites();
  }
}
