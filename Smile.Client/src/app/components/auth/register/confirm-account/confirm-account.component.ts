import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-confirm-account',
  templateUrl: './confirm-account.component.html',
  styleUrls: ['./confirm-account.component.scss']
})
export class ConfirmAccountComponent implements OnInit {
  userId: string;
  token: string;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute, private notifier: Notifier) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParams.userId;
    this.token = this.route.snapshot.queryParams.token;

    if (this.userId && this.token) {
      this.redirect();
    } else {
      this.router.navigate(['']);
    }
  }

  private redirect() {
    this.authService.confirmAccount(this.userId, this.token).subscribe(() => {
      this.notifier.push('Account has been activated', 'success');
      this.router.navigate(['/auth']);
    }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['']);
    });
  }
}
