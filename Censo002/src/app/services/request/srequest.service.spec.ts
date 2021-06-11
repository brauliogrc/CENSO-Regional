import { TestBed } from '@angular/core/testing';

import { SrequestService } from './srequest.service';

describe('SrequestService', () => {
  let service: SrequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SrequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
