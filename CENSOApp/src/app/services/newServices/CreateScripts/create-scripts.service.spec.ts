import { TestBed } from '@angular/core/testing';

import { CreateScriptsService } from './create-scripts.service';

describe('CreateScriptsService', () => {
  let service: CreateScriptsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateScriptsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
