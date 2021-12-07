import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PanelusuariobusqComponent } from './panelusuariobusq.component';

describe('PanelusuariobusqComponent', () => {
  let component: PanelusuariobusqComponent;
  let fixture: ComponentFixture<PanelusuariobusqComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PanelusuariobusqComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PanelusuariobusqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
