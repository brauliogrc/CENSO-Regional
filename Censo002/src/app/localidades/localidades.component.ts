import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import { locationList, addLocation } from '../../assets/ts/interfaces/newInterfaces';
import { ListService } from '../services/newServices/List/list.service';
import { LocationService } from '../services/newServices/Location/location.service';
import { SearchService } from '../services/newServices/Search/search.service';

import { Popup } from 'src/assets/ts/popup';

@Component({
  selector: 'app-localidades',
  templateUrl: './localidades.component.html',
  styleUrls: ['./localidades.component.css'],
})
export class LocalidadesComponent implements OnInit {
  // Array que contiene la informacion de las localidades disponibles para ser mostradas en la tabla
  Locations: locationList[] = [];

  // Toma los valores de la localidad buscada por medio del id
  location: any;

  newLocation = this._fb.group({
    lName: ['', [Validators.required, Validators.maxLength(50)]],
    lStatus: ['', [Validators.required]],
  });

  constructor(
    private _listService: ListService,
    private _locationService: LocationService,
    private _fb: FormBuilder,
    private _searchService: SearchService,
    private _authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  // Validación del rol
  validRole(): void {
    if (Number(sessionStorage.getItem('role')) != 1) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getLocationList();
  }

  // Obtencion del listado de localidades dispoibles
  getLocationList(): void {
    this._listService.getLocationList().subscribe(
      (data) => {
        this.Locations = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Registrar una nueva localidad en la tabla locations de la base de datos
  addNewLocation(): void {
    const newLocation: addLocation = {
      lName: this.newLocation.get('lName')?.value,
      lCreationUser: Number(sessionStorage.getItem('userId')),
      lStatus: this.newLocation.get('lStatus')?.value,
    };
    console.log(newLocation);

    this._locationService.addNewLocation(newLocation).subscribe(
      (data) => {
        console.log(data.message);
        this.getLocationList();
        this.newLocation.reset();
        this.location = null;
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Borrado logico de la localidad de la tabla locations
  deleteLocation(locationId: number): void {
    this._locationService.deleteLocaion(locationId).subscribe(
      (data) => {
        console.log(data.message);
        this.location = null;
        this.getLocationList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Busca una localidad con base al id dado
  search(locationId: any): void {
    this._searchService.searchLocation(Number(locationId)).subscribe(
      (data) => {
        this.location = data;
        this.Locations = [];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Llamado de modals
  private popup = new Popup();

  mostrar() {
    this.popup.mostrar();
  }

  cerrar() {
    this.popup.cerrar();
  }
}
