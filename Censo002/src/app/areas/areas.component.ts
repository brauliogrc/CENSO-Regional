import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styleUrls: ['./areas.component.css'],
})
export class AreasComponent implements OnInit {
  areas: any[] = []; // Asignar a una interface

  constructor(
    private _authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2
    ) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getAllAreas();
  }

  getAllAreas() {
    // this._service.tableAreas().subscribe(
    //   (data) => {
    //     console.log(data);
    //     this.areas = [...data];
    //   },
    //   (error) => {
    //     console.error('Error getting data ' + error);
    //   }
    // );
  }
}
