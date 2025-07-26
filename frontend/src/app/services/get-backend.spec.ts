import { TestBed } from '@angular/core/testing';

import { GetBackend } from './get-backend';

describe('GetBackend', () => {
  let service: GetBackend;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetBackend);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
