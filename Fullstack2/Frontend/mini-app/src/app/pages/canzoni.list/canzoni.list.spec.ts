import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CanzoniList } from './canzoni.list';

describe('CanzoniList', () => {
  let component: CanzoniList;
  let fixture: ComponentFixture<CanzoniList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CanzoniList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CanzoniList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
