import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import { AreaService } from '../services/newServices/Area/area.service';
import { ListService } from '../services/newServices/List/list.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { FormBuilder, Validators } from '@angular/forms';
import {
  areaList,
  Location,
  searchData,
  addArea,
  existingArea,
  itemChanges,
} from '../../assets/ts/interfaces/newInterfaces';

import { Popup } from 'src/assets/ts/popup';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styleUrls: ['./areas.component.css'],
})
export class AreasComponent implements OnInit {
  Areas: areaList[] = []; // Asignar a una interface
  area: any;

  Locations: Location[] = [];

  constructor(
    private _showError: ShowErrorService,
    private _authService: AuthService,
    private _areaService: AreaService,
    private _listService: ListService,
    private _searchService: SearchService,
    private _fieldsService: FieldsService,
    private _fb: FormBuilder,
    private router: Router
  ) {}

  newArea = this._fb.group({
    aName: ['', [Validators.required, Validators.maxLength(50)]],
    LocationId: ['', [Validators.required]],
    aStatus: ['', [Validators.required]],
  });

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2
    ) {
      console.error('Sección no accesible');
      this._showError.NotAccessible();
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getAreasList();
    this.getLocations();
  }

  // Obtención de las areas disponibles
  getLocations(): void {
    this._fieldsService.getLocations().subscribe(
      (data) => {
        this.Locations = [...data];
        // console.log(this.Locations);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtencion de las areas disponibles
  getAreasList(): void {
    this._listService
      .getAreaList(Number(sessionStorage.getItem('location')))
      .subscribe(
        (data) => {
          this.Areas = [...data];
          // console.log(this.Areas);
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
  }

  addNewArea(): void {
    const dataNewArea: addArea = {
      aName: this.newArea.get('aName')?.value,
      LocationId: this.newArea.get('LocationId')?.value,
      aStatus: this.newArea.get('aStatus')?.value,
    };

    // console.log(dataNewArea);

    this._areaService.addNewArea(dataNewArea).subscribe(
      (data) => {
        // console.log(data.message);
        this._showError.success(data.message);
        this.getAreasList();
        this.newArea.reset();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Borrado lógico de un area
  deleteArea(areaId: number): void {
    this._areaService.deleteArea(areaId).subscribe(
      (data) => {
        // console.log(data.message);
        this._showError.success(data.message);
        this.area = null;
        this.getAreasList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  searchArea(areaName: string): void {
    if (areaName) {
      // Definicion de os datos de busqueda
      let areaSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: areaName,
      };

      this._searchService.searchArea(areaSearch).subscribe(
        (data) => {
          this.Areas = [...data];
          // console.log(this.area);
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message + ' ddd');
          this._showError.statusCode(error);
        }
      );
    }
  }

  // Llamado de modals y actualizacion de areas
  private popup = new Popup();
  areaData: existingArea;
  editFlag: boolean = false;
  mostrar(areaId: number) {
    this.getExistingArea(areaId);
    this.popup.mostrar();
  }
  cerrar() {
    this.updateArea.reset();
    this.editFlag = false;
    this.popup.cerrar();
  }

  // Obtención de los datos del area a actualizar
  getExistingArea(areaId: number): void {
    this._searchService.getExistingArea(areaId).subscribe(
      (data) => {
        this.areaData = data[0];
        this.editFlag = true;
        // console.log(this.areaData);
      },
      (error: HttpErrorResponse) => {
        this._showError.statusCode(error);
      }
    );
  }

  updateArea = this._fb.group({
    newName: [''],
    newStatus: [''],
    newLocation: [''],
  });

  // Guardado de los cambios en el area
  saveChanges() {
    const saveChanges: itemChanges = {
      modificationUser: Number(sessionStorage.getItem('employeeNumber')),
      itemId: this.areaData.aId,
      itemName: this.updateArea.get('newName').value,
      itemStatus: this.updateArea.get('newStatus').value,
      locationId: this.updateArea.get('newLocation').value,
    };

    // console.log(saveChanges);

    this._areaService.areaUpdate(saveChanges).subscribe(
      (data) => {
        this._showError.success(data.message);
        this.getAreasList();
        this.updateArea.reset();
      },
      (error: HttpErrorResponse) => {
        this._showError.statusCode(error);
      }
    );
  }
}
