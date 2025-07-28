import { TestBed } from '@angular/core/testing';

import { Canzoni } from './canzoni';

describe('Canzoni', () => {
  let service: Canzoni;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Canzoni);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
