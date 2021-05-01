import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { SortType } from 'src/app/enums/sort-type.enum';
import { shortenStr } from 'src/app/helpers/helpers';
import { Log } from 'src/app/models/domain/log/log';
import { Pagination } from 'src/app/models/helpers/pagination';
import { LogsRequest } from 'src/app/resolvers/requests/logs-request';
import { LogsService } from 'src/app/services/logs.service';
import { Notifier } from 'src/app/services/notifier.service';
import { colors, constants } from 'src/environments/environment';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {
  logs: Log[];
  pagination: Pagination;

  logsRequest = new LogsRequest();

  filtersForm: FormGroup;

  colors = colors;

  shortenStr = shortenStr;
  localHostLength = constants.localHostLength;

  dateMinPicker: MatDatepicker<Date>;
  dateMaxPicker: MatDatepicker<Date>;

  minDate = new Date(new Date().setMonth(new Date().getMonth() - 3));
  maxDate = new Date();

  sortType = SortType;

  constructor(private logsService: LogsService, private route: ActivatedRoute, private formBuilder: FormBuilder,
              private notifier: Notifier) { }

  ngOnInit(): void {
    this.createFiltersForm();
    this.subscribeData();
  }

  public filterLogs(pageNumber = 1) {
    if (pageNumber !== this.pagination.currentPage) {
      this.logsRequest.pageNumber = pageNumber;
    }

    this.checkDateControls();

    this.logsRequest = Object.assign(this.logsRequest, this.filtersForm.value);

    this.logsService.getLogs(this.logsRequest).subscribe(response => {
      this.logs = response.result.logs;
      this.pagination = response.pagination;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public resetFilters() {
    this.logsRequest = new LogsRequest();
    this.createFiltersForm();
    this.filterLogs();
  }

  public async nextPage(page: PageEvent) {
    await this.filterLogs(page.pageIndex + 1);
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      minDate: [this.minDate],
      maxDate: [this.maxDate],
      level: [''],
      message: [''],
      url: [''],
      action: [''],
      sortType: [SortType.Descending]
    });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.logs = data.logsResponse.result.logs;
      this.pagination = data.logsResponse.pagination;
    });
  }

  private checkDateControls() {
    const minDateControl = this.filtersForm.get('minDate');
    const maxDateControl = this.filtersForm.get('maxDate');

    if (minDateControl.value < this.minDate || minDateControl.value > this.maxDate) {
      minDateControl.setValue(this.minDate);
    }

    if (maxDateControl.value < this.minDate || maxDateControl.value > this.maxDate) {
      maxDateControl.setValue(this.maxDate);
    }
  }
}
