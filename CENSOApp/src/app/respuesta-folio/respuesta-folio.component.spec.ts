import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RespuestaFolioComponent } from './respuesta-folio.component';

describe('RespuestaFolioComponent', () => {
  let component: RespuestaFolioComponent;
  let fixture: ComponentFixture<RespuestaFolioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RespuestaFolioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RespuestaFolioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
