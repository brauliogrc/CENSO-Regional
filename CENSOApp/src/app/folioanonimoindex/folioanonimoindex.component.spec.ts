import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolioanonimoindexComponent } from './folioanonimoindex.component';

describe('FolioanonimoindexComponent', () => {
  let component: FolioanonimoindexComponent;
  let fixture: ComponentFixture<FolioanonimoindexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FolioanonimoindexComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FolioanonimoindexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
