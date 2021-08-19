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
} from '../../assets/ts/interfaces/newInterfaces';

import { Popup } from 'src/assets/ts/popup';

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
        console.log(this.Locations);
      },
      (error) => {
        console.error(error.error.message);
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
          console.log(this.Areas);
        },
        (error) => {
          console.error(error.error.message);
        }
      );
  }

  addNewArea(): void {
    const dataNewArea: addArea = {
      aName: this.newArea.get('aName')?.value,
      LocationId: this.newArea.get('LocationId')?.value,
      aStatus: this.newArea.get('aStatus')?.value,
    };

    this._areaService.addNewArea(dataNewArea).subscribe(
      (data) => {
        console.log(data.message);
        this.getAreasList();
        this.newArea.reset();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Borrado lógico de un area
  deleteArea(areaId: number): void {
    this._areaService.deleteArea(areaId).subscribe(
      (data) => {
        console.log(data.message);
        this.area = null;
        this.getAreasList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  searchArea(areaId: string): void {
    if (areaId) {
      // Definicion de os datos de busqueda
      let areaSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: Number(areaId),
      };

      this._searchService.searchArea(areaSearch).subscribe(
        (data) => {
          this.area = data;
          this.Areas = [];
          console.log(this.area);
        },
        (error) => {
          console.error(error.error.message + ' ddd');
        }
      );
    }
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
