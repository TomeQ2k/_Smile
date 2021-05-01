import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendButtonsComponent } from './friend-buttons.component';

describe('FriendButtonsComponent', () => {
  let component: FriendButtonsComponent;
  let fixture: ComponentFixture<FriendButtonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FriendButtonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FriendButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
