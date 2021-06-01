import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolioanonimoComponent } from './folioanonimo.component';

describe('FolioanonimoComponent', () => {
  let component: FolioanonimoComponent;
  let fixture: ComponentFixture<FolioanonimoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FolioanonimoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FolioanonimoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
