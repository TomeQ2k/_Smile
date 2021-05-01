import { TestBed } from '@angular/core/testing';
import { ConnectionManager } from './connection-manager.service';

describe('ConnectionManager', () => {
  let service: ConnectionManager;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConnectionManager);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
