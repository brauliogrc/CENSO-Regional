import { TestBed } from '@angular/core/testing';

import { SthemeService } from './stheme.service';

describe('SthemeService', () => {
  let service: SthemeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SthemeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
