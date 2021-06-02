import { TestBed } from '@angular/core/testing';

import { SquestionsService } from './squestions.service';

describe('SquestionsService', () => {
  let service: SquestionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SquestionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
