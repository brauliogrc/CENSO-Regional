import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import {
  addTheme,
  themeList,
  Location,
  searchData,
  addThemeRelationship,
  existingTheme,
  itemChanges,
} from '../../assets/ts/interfaces/newInterfaces';
import { ThemeService } from '../services/newServices/Theme/theme.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { ListService } from '../services/newServices/List/list.service';
import { SearchService } from '../services/newServices/Search/search.service';

import { Popup } from 'src/assets/ts/popup';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-temas',
  templateUrl: './temas.component.html',
  styleUrls: ['./temas.component.css'],
})
export class TemasComponent implements OnInit {
  // Array que conendrás los datos de los temas para mostrarlos en la tabla
  Theme: themeList[] = [];
  private respaldo: themeList[] = [];

  th: any;

  // Array con las localidades disponibles para la lista desplegable
  Locations: Location[] = [];

  newTheme = this._fb.group({
    tName: ['', [Validators.required, Validators.maxLength(50)]],
    LocationId: ['', [Validators.required]],
    tStatus: ['', [Validators.required]],
  });

  constructor(
    private router: Router,
    private _fb: FormBuilder,
    private _listService: ListService,
    private _authService: AuthService,
    private _themeService: ThemeService,
    private _showErrors: ShowErrorService,
    private _fieldsService: FieldsService,
    private _searchService: SearchService
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
      this._showErrors.NotAccessible();
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getThemeList();
    this.getLocations();
  }

  // Obtencion de las localidas disponibles
  getLocations(): void {
    this._fieldsService.getLocations().subscribe(
      (data) => {
        this.Locations = [...data];
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showErrors.statusCode(error);
      }
    );
  }

  // Obtencion de los temas disponibles
  getThemeList(): void {
    this._listService
      .getThemeList(Number(sessionStorage.getItem('location')))
      .subscribe(
        (data) => {
          this.Theme = [...data];
          this.respaldo = [...data];
          // console.log(this.Theme);
          
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showErrors.statusCode(error);
        }
      );
  }

  // Registrar un nuevo tema en la tabla Theme
  addNewTheme(): void {
    const dataNewTheme: addTheme = {
      tName: this.newTheme.get('tName')?.value,
      tStatus: this.newTheme.get('tStatus')?.value,
      tCreationUser: Number(sessionStorage.getItem('userId')),
      LocationId: this.newTheme.get('LocationId')?.value,
    };

    this._themeService.addNewTheme(dataNewTheme).subscribe(
      (data) => {
        // console.log(data.message);
        this._showErrors.success(data.message);
        this.getThemeList();
        this.newTheme.reset();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showErrors.statusCode(error);
      }
    );
  }

  // Borrado logico de un tema
  deleteTheme(themeId: number): void {
    this._themeService.deleteTheme(themeId).subscribe(
      (data) => {
        // console.log(data.message);
        this._showErrors.success(data.message);
        this.th = null;
        this.getThemeList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showErrors.statusCode(error);
      }
    );
  }

  // Busqueda de un tema en especifico
  search(themeName: string): void {
    if (themeName) {
      // Definicioin de los datos de busquedas
      let themeSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: themeName,
      };

      this._searchService.searchTheme(themeSearch).subscribe(
        (data) => {
          this.Theme = [...data];
          // this.Theme = [];
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.messae);
          this._showErrors.statusCode(error);
        }
      );
    }
  }

  // Limpiado del filtrado de la tabla
  public clearFilter(): void {
    this.Theme = [...this.respaldo];
  }

  // Llamado de modals y actualizacion del tema
  private popup = new Popup();
  themeData: existingTheme;
  relatedLocations: Location[] = [];
  editFlag: boolean = false;

  mostrar(themeId: number) {
    this.getExistingTheme(themeId);
    this.popup.mostrar();
  }

  cerrar() {
    this.updateTheme.reset();
    this.editFlag = false;
    this.popup.cerrar();
  }

  mostrarTema(themeId: number) {
    this.getExistingTheme(themeId);
    this.getRelatedLocations(themeId);
    this.popup.mostrarTema();
  }

  cerrarTema() {
    this.popup.cerrarTema();
  }

  // Obtencion de los datos a modificar del tema
  getExistingTheme(themeId: number): void {
    this._searchService.getExistingTheme(themeId).subscribe(
      (data) => {
        this.themeData = data[0];
        // console.log(this.themeData);
        this.editFlag = true;
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showErrors.statusCode(error);
      }
    );
  }

  updateTheme = this._fb.group({
    newName: ['', [Validators.maxLength(50)]],
    newStatus: [''],
  });

  // Guardado de los cambios en el tema
  saveChanges() {
    const saveChanges: itemChanges = {
      modificationUser: Number(sessionStorage.getItem('employeeNumber')),
      itemId: this.themeData.tId,
      itemName: this.updateTheme.get('newName').value,
      itemStatus: this.updateTheme.get('newStatus').value,
      locationId: null
    };

    // console.log(saveChanges);
    

    this._themeService.themeUpdate(saveChanges).subscribe(
      (data) => {
        // console.log(data.message);
        this._showErrors.success(data.message);
        this.getThemeList();
        this.updateTheme.reset();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showErrors.statusCode(error);
      }
    );
  }

  // Obtencion de las localidades relaciondad al tema
  getRelatedLocations(themeId: number) {
    this._searchService.getRelatedLocations(themeId).subscribe(
      (data) => {
        this.relatedLocations = [...data];
        // console.log(this.relatedLocations);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showErrors.statusCode(error);
      }
    );
  }

  // Obtencion del idex de la localidad e eliminar
  getIdx(locationId: number) {
    this.relatedLocations.find((item, idx) => {
      if (item.lId === locationId) {
        // console.log(item, idx);
        // console.log(locationId);
        
        this.deleteRelatedLocation(locationId);
      }
    });
  }

  // Borrado de una relacion entre el tema y la localidad
  deleteRelatedLocation(locationId: number) {
    this._themeService
      .deleteRelatedLocation(locationId, this.themeData.tId)
      .subscribe(
        (data) => {
          // console.log(data.message);
          this._showErrors.success(data.message);
          this.getRelatedLocations(this.themeData.tId);
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showErrors.statusCode(error);
        }
      );
  }

  // Añadir una relacion entre el tema y la localidad
  addRelatedLocation(locationId: number): void {
    const newRelationship: addThemeRelationship = {
      itemId: locationId,
      themeId: this.themeData.tId,
    };

    this._themeService.addRelatedLocation(newRelationship).subscribe(
      (data) => {
        // console.log(data.message);
        this._showErrors.success(data.message);
        this.getRelatedLocations(this.themeData.tId);
      },
      (error: HttpErrorResponse) => {
        this._showErrors.statusCode(error);
        console.error(error.error.message);
      }
    );
  }
}
