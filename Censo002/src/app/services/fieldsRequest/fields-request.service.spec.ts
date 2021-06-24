import { TestBed } from '@angular/core/testing';

import { FieldsRequestService } from './fields-request.service';

describe('FieldsRequestService', () => {
  let service: FieldsRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FieldsRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
