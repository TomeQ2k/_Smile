import { TestBed } from '@angular/core/testing';

import { GroupManager } from './group-manager.service';

describe('GroupManagerService', () => {
  let service: GroupManager;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GroupManager);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
