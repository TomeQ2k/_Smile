import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Notifier } from 'src/app/services/notifier.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['./change-email.component.scss']
})
export class ChangeEmailComponent implements OnInit {
  userId: string;
  token: string;
  newEmail: string;

  constructor(private profileService: ProfileService, private route: ActivatedRoute, private router: Router, private notifier: Notifier) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = params.userId;
      this.token = params.token;
      this.newEmail = params.newEmail;
    });

    this.changeEmail();
  }

  private changeEmail() {
    if (this.userId && this.token && this.newEmail) {
      this.profileService.changeEmail(this.userId, this.token, this.newEmail).subscribe(() => {
        this.notifier.push('Email changed', 'success');
        this.router.navigate(['/profile']);
      });
    } else {
      this.router.navigate(['']);
    }
  }
}
