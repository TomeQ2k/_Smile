import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SortType } from 'src/app/enums/sort-type.enum';
import { ReportsRequest } from 'src/app/resolvers/requests/reports-request';

@Component({
  selector: 'app-reports-filters',
  templateUrl: './reports-filters.component.html',
  styleUrls: ['./reports-filters.component.scss']
})
export class ReportsFiltersComponent implements OnInit {
  filtersForm: FormGroup;

  reportsRequest: ReportsRequest = new ReportsRequest();

  @Input() isAdmin: boolean;

  @Output() filtersChanged = new EventEmitter<any>();

  sortType = SortType;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.createFiltersForm();
  }

  public onFiltersChanged() {
    Object.assign(this.reportsRequest, this.filtersForm.value);
    this.filtersChanged.emit(this.reportsRequest);
  }

  public resetFilters() {
    this.reportsRequest = new ReportsRequest();
    this.createFiltersForm();
    this.filtersChanged.emit(this.reportsRequest);
  }

  private createFiltersForm() {
    this.filtersForm = this.formBuilder.group({
      reportStatus: [0],
      sortType: [SortType.Descending],
      reportType: [0],
      reporterName: ['']
    });
  }
}
