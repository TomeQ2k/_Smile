import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ReportList } from 'src/app/models/domain/support/report-list';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Notifier } from 'src/app/services/notifier.service';
import { SupportService } from 'src/app/services/support.service';

@Component({
  selector: 'app-report-card',
  templateUrl: './report-card.component.html',
  styleUrls: ['./report-card.component.scss']
})
export class ReportCardComponent implements OnInit {
  @Input() report: ReportList;

  @Input() isAdmin: boolean;
  @Input() isHeadAdmin: boolean;

  @Output() reportDeleted = new EventEmitter<DeleteEmitter>();

  constructor(private supportService: SupportService, private notifier: Notifier) { }

  ngOnInit(): void {
  }

  public toggleReportStatus() {
    this.supportService.toggleReportStatus(this.report.id).subscribe(() => {
      const isClosed = this.report.isClosed;
      this.notifier.push(`Report #${this.report.id} has been ${isClosed ? 'opened' : 'closed'}`, 'info');
      this.report.isClosed = !isClosed;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public deleteReport() {
    if (confirm(`Are you sure you want to delete report #${this.report.id}?`)) {
      this.supportService.deleteReport(this.report.id).subscribe(() => {
        this.notifier.push(`Report #${this.report.id} has been deleted`, 'info');
        this.reportDeleted.emit({ deleted: true, objectId: this.report.id });
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }
}
