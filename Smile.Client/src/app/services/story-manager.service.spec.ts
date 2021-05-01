import { TestBed } from '@angular/core/testing';

import { StoryManagerService } from './story-manager.service';

describe('StoryManagerService', () => {
  let service: StoryManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StoryManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
