import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import {
  locationList,
  addLocation,
  existingLocation,
} from '../../assets/ts/interfaces/newInterfaces';
import { ListService } from '../services/newServices/List/list.service';
import { LocationService } from '../services/newServices/Location/location.service';
import { SearchService } from '../services/newServices/Search/search.service';

import { Popup } from 'src/assets/ts/popup';
import { itemChanges } from '../../assets/ts/interfaces/newInterfaces';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';

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
    private router: Router,
    private _fb: FormBuilder,
    private _listService: ListService,
    private _authService: AuthService,
    private _showError: ShowErrorService,
    private _searchService: SearchService,
    private _locationService: LocationService
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  // Validación del rol
  validRole(): void {
    if (Number(sessionStorage.getItem('role')) != 1) {
      console.error('Sección no accesible');
      this._showError.NotAccessible();
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
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
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
    // console.log(newLocation);

    this._locationService.addNewLocation(newLocation).subscribe(
      (data) => {
        // console.log(data.message);
        this._showError.success(data.message);
        this.getLocationList();
        this.newLocation.reset();
        this.location = null;
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Borrado logico de la localidad de la tabla locations
  deleteLocation(locationId: number): void {
    this._locationService.deleteLocaion(locationId).subscribe(
      (data) => {
        // console.log(data.message);
        this._showError.success(data.message);
        this.location = null;
        this.getLocationList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Busca una localidad con base al id dado
  search(locationName: string): void {
    this._searchService.searchLocation(locationName).subscribe(
      (data) => {
        this.location = data;
        this.Locations = [];
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Llamado de modals y actualización de la localidad.
  private popup = new Popup();
  locationData: existingLocation;
  editFlag: boolean = false;

  mostrar(locationId: number) {
    this.getLocationData(locationId);
    this.popup.mostrar();
  }

  cerrar() {
    this.editFlag = false;
    this.updateLocation.reset();
    this.popup.cerrar();
  }

  getLocationData(locationId: number): void {
    this._searchService.getExistingLocation(locationId).subscribe(
      (data) => {
        this.locationData = data[0];
        this.editFlag = true;
        // console.log(this.locationData);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  updateLocation = this._fb.group({
    newName: ['', [Validators.maxLength(50)]],
    newStatus: [''],
  });

  // Método de modificación de los campos de la localidad
  saveChanges(): void {
    const saveChanges: itemChanges = {
      modificationUser: Number(sessionStorage.getItem('employeeNumber')),
      itemId: this.locationData.lId,
      itemName: this.updateLocation.get('newName').value,
      itemStatus: this.updateLocation.get('newStatus').value,
      locationId: null
    };
    // console.log(saveChanges);

    this._locationService.locatinoUpdate(saveChanges).subscribe(
      (data) => {
        // console.log(data.message);
        this._showError.success(data.message);
        this.getLocationList();
        this.updateLocation.reset();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }
}
