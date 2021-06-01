import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FvacioComponent } from './fvacio.component';

describe('FvacioComponent', () => {
  let component: FvacioComponent;
  let fixture: ComponentFixture<FvacioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FvacioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FvacioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
