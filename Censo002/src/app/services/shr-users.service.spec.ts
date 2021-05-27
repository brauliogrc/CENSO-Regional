import { TestBed } from '@angular/core/testing';

import { ShrUsersService } from './shr-users.service';

describe('ShrUsersService', () => {
  let service: ShrUsersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShrUsersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
