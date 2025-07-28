import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AscoltoDetail } from './ascolto-detail';

describe('AscoltoDetail', () => {
  let component: AscoltoDetail;
  let fixture: ComponentFixture<AscoltoDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AscoltoDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AscoltoDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
