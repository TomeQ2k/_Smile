import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateReportAnonymousComponent } from './create-report-anonymous.component';

describe('CreateReportAnonymousComponent', () => {
  let component: CreateReportAnonymousComponent;
  let fixture: ComponentFixture<CreateReportAnonymousComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateReportAnonymousComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateReportAnonymousComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
