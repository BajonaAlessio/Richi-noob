import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CanzoneDetail } from './canzone-detail';

describe('CanzoneDetail', () => {
  let component: CanzoneDetail;
  let fixture: ComponentFixture<CanzoneDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CanzoneDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CanzoneDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
