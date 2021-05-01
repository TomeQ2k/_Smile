import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportList } from 'src/app/models/domain/support/report-list';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { ReportsRequest } from 'src/app/resolvers/requests/reports-request';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { SupportService } from 'src/app/services/support.service';
import { roles } from 'src/environments/environment';

@Component({
  selector: 'app-support-panel',
  templateUrl: './support-panel.component.html',
  styleUrls: ['./support-panel.component.scss']
})
export class SupportPanelComponent implements OnInit {
  reports: ReportList[];
  pagination: Pagination;

  reportsRequest: ReportsRequest = new ReportsRequest();

  isAdmin: boolean;
  isHeadAdmin: boolean;

  constructor(private supportService: SupportService, private route: ActivatedRoute, private authService: AuthService,
              private router: Router, private notifier: Notifier) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public async nextPage(page: PageEvent) {
    await this.filterReports(page.pageIndex + 1);
  }

  public onFiltersChanged(reportsRequest: ReportsRequest) {
    this.reportsRequest = reportsRequest;
    this.filterReports();
  }

  public onDeleteReport(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.reports = this.reports.filter(r => r.id !== emitter.objectId);
    }
  }

  private filterReports(pageNumber = 1) {
    if (this.reportsRequest.pageNumber !== pageNumber) {
      this.reportsRequest.pageNumber = pageNumber;
    }

    if (this.isAdmin) {
      this.supportService.fetchAllReports(this.reportsRequest).subscribe(response => {
        this.reports = response?.result.reports;
        this.pagination = response?.pagination;
      }, error => {
        this.notifier.push(error, 'error');
      });
    } else {
      this.supportService.fetchReports(this.reportsRequest).subscribe(response => {
        this.reports = response?.result.reports;
        this.pagination = response?.pagination;
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.reports = data.reportsResponse.result.reports;
      this.pagination = data.reportsResponse.pagination;

      this.isAdmin = this.authService.checkPermissions(roles.adminRoles);
      this.isHeadAdmin = this.authService.checkPermissions([roles.headAdminRole]);

      if (!data.isAdmin && this.isAdmin) {
        this.router.navigate(['/admin/support']);
      }
    });
  }
}
