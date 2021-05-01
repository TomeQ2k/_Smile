import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RedirectAdminPanelComponent } from './redirect-admin-panel.component';

describe('RedirectAdminPanelComponent', () => {
  let component: RedirectAdminPanelComponent;
  let fixture: ComponentFixture<RedirectAdminPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RedirectAdminPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RedirectAdminPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
