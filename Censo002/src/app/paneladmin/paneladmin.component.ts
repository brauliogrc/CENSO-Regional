import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { saveDataLogin } from '../interfaces/interfaces';
import { AuthService } from '../services/Auth/auth.service';

@Component({
  selector: 'app-paneladmin',
  templateUrl: './paneladmin.component.html',
  styleUrls: ['./paneladmin.component.css'],
})
export class PaneladminComponent implements OnInit {
  constructor(private router: Router, private _authService: AuthService) { }

  private user: saveDataLogin = {
    uId: 0,
    uEmail: '',
    uName: '',
    locationId: 0,
    roleId: 0,
  };

  ngOnInit(): void {
    this.user = this._authService.getUser();
    this.getName();
    this.getRole();
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
  navegarReportes() {
    this.router.navigate(['paneladmin/Reportes']);
  }

  getName() {
    return sessionStorage.getItem('username');
  }

  getRole() {
    return Number(sessionStorage.getItem('role'));
  }

  logout() {
    this._authService.logout();
  }
}
