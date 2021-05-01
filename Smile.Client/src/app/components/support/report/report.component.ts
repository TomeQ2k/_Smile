import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Report } from 'src/app/models/domain/support/report';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  report: Report;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public onSendReply(emitter: PutEmitter) {
    if (!emitter.updated) {
      this.report.replies.unshift(emitter.object);
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => this.report = data.reportResponse.report);
  }
}
