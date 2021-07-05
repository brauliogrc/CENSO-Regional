import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-paneladmin',
  templateUrl: './paneladmin.component.html',
  styleUrls: ['./paneladmin.component.css']
})
export class PaneladminComponent implements OnInit {

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  navegarUsuarios() {
    this.router.navigate(['paneladmin/usuarios']);
  }
  navegarLocalidades() {
    this.router.navigate(['paneladmin/localidades']);
  }
  navegarTemas() {
    this.router.navigate(['paneladmin/temas']);
  }
  navegarTikets() {
    this.router.navigate(['paneladmin/tikets']);
  }
  navegarAreas() {
    this.router.navigate(['paneladmin/areas']);
  }
  navegarPreguntas() {
    this.router.navigate(['paneladmin/preguntas']);
  }

}
