import { TestBed } from '@angular/core/testing';

import { FormHelper } from './form-helper.service';

describe('ErrorHelperService', () => {
  let service: FormHelper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormHelper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
