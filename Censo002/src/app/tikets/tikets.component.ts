import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tikets',
  templateUrl: './tikets.component.html',
  styleUrls: ['./tikets.component.css'],
})
export class TiketsComponent implements OnInit {
  Tikets: any[] = [];
  AnonTikets: any[] = [];

  tiket: any;

  constructor(private _auth: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2 &&
      Number(sessionStorage.getItem('role')) != 3
    ) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getTiketsList();
  }

  // Obtencion de listado de tikets (tanto anonimos como con datos)
  getTiketsList() {
    // this._dataTable
    //   .tableTickets(Number(sessionStorage.getItem('location')))
    //   .subscribe(
    //     (data) => {
    //       console.log(data);
    //       this.Tikets = [...data.tikets];
    //       this.AnonTikets = [...data.anonTikets];
    //     },
    //     (error) => {
    //       console.error(error);
    //     }
    //   );
  }

  // Busqueda de un tiket mediante su id
  search(tiketId: any) {}

  // Obtencion de los datos de la tabla al hacer click en una row
  onClick(requestiId: any) {
    console.log('Prueba de clik ' + requestiId);
  }
}
