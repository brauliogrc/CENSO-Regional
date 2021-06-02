import { TestBed } from '@angular/core/testing';

import { SlocationsService } from './slocations.service';

describe('SlocationsService', () => {
  let service: SlocationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlocationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
