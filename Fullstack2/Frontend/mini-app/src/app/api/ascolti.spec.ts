import { TestBed } from '@angular/core/testing';

import { Ascolti } from './ascolti';

describe('Ascolti', () => {
  let service: Ascolti;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Ascolti);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
