import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AscoltiList } from './ascolti.list';

describe('AscoltiList', () => {
  let component: AscoltiList;
  let fixture: ComponentFixture<AscoltiList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AscoltiList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AscoltiList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
