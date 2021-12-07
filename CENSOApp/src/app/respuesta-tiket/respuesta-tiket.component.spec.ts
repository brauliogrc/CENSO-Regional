import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RespuestaTiketComponent } from './respuesta-tiket.component';

describe('RespuestaTiketComponent', () => {
  let component: RespuestaTiketComponent;
  let fixture: ComponentFixture<RespuestaTiketComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RespuestaTiketComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RespuestaTiketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
