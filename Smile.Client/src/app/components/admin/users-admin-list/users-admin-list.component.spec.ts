import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersAdminListComponent } from './users-admin-list.component';

describe('UsersAdminListComponent', () => {
  let component: UsersAdminListComponent;
  let fixture: ComponentFixture<UsersAdminListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersAdminListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersAdminListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
