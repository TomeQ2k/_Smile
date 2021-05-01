import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/helpers/form-helper.service';
import { Notifier } from 'src/app/services/notifier.service';
import { SupportService } from 'src/app/services/support.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-create-report',
  templateUrl: './create-report.component.html',
  styleUrls: ['./create-report.component.scss']
})
export class CreateReportComponent implements OnInit {
  reportForm: FormGroup;

  attachments: File[] = [];

  constants = constants;

  constructor(private supportService: SupportService, private formBuilder: FormBuilder, public formHelper: FormHelper,
              private notifier: Notifier, private router: Router) { }

  ngOnInit(): void {
    this.createReportForm();
  }

  public sendReport() {
    if (this.reportForm.valid) {
      this.supportService.createReport(this.reportForm.value.subject, this.reportForm.value.content, this.attachments)
        .subscribe(() => {
          this.notifier.push('Report has been sent', 'success');
          this.router.navigate(['support']);
        }, error => {
          this.notifier.push(error, 'error');
        });
    }
  }

  public onChangeAttachments(files: File[]) {
    this.attachments = files;
  }

  private createReportForm() {
    this.reportForm = this.formBuilder.group({
      subject: ['', [Validators.required, Validators.maxLength(constants.titleLength)]],
      content: ['', [Validators.required, Validators.maxLength(constants.contentLength)]]
    });
  }
}
