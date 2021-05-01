import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportsFiltersComponent } from './reports-filters.component';

describe('ReportsFiltersComponent', () => {
  let component: ReportsFiltersComponent;
  let fixture: ComponentFixture<ReportsFiltersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportsFiltersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportsFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
