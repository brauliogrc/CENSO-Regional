import { TestBed } from '@angular/core/testing';

import { AddAnonRequestService } from './add-anon-request.service';

describe('AddAnonRequestService', () => {
  let service: AddAnonRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddAnonRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
