import { TestBed } from '@angular/core/testing';

import { SareasService } from './sareas.service';

describe('SareasService', () => {
  let service: SareasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SareasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
