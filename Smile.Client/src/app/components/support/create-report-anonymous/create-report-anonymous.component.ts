import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Notifier } from 'src/app/services/notifier.service';
import { SupportService } from 'src/app/services/support.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-create-report-anonymous',
  templateUrl: './create-report-anonymous.component.html',
  styleUrls: ['./create-report-anonymous.component.scss']
})
export class CreateReportAnonymousComponent implements OnInit {
  reportForm: FormGroup;

  constants = constants;

  constructor(private supportService: SupportService, private notifier: Notifier, public formHelper: FormHelper,
              private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.createReportForm();
  }

  public sendReportAnonymous() {
    if (this.reportForm.valid) {
      this.supportService.createAnonymousReport(this.reportForm.value.subject, this.reportForm.value.content, this.reportForm.value.email)
        .subscribe(() => {
          this.notifier.push(`Report has been sent. Reply will be sent to the address: ${this.reportForm.value.email}`, 'success');
          this.router.navigate(['support']);
        }, error => {
          this.notifier.push(error, 'error');
        });
    }
  }

  private createReportForm() {
    this.reportForm = this.formBuilder.group({
      subject: ['', [Validators.required, Validators.maxLength(constants.titleLength)]],
      email: ['', [Validators.required, Validators.email]],
      content: ['', [Validators.required, Validators.maxLength(constants.contentLength)]]
    });
  }
}
