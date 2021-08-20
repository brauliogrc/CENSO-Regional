import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import {
  addTheme,
  themeList,
  Location,
  searchData,
} from '../../assets/ts/interfaces/newInterfaces';
import { ThemeService } from '../services/newServices/Theme/theme.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { ListService } from '../services/newServices/List/list.service';
import { SearchService } from '../services/newServices/Search/search.service';

import { Popup } from 'src/assets/ts/popup';
import {
  existingTheme,
  itemChanges,
} from '../../assets/ts/interfaces/newInterfaces';

@Component({
  selector: 'app-temas',
  templateUrl: './temas.component.html',
  styleUrls: ['./temas.component.css'],
})
export class TemasComponent implements OnInit {
  // Array que conendrás los datos de los temas para mostrarlos en la tabla
  Theme: themeList[] = [];

  th: any;

  // Array con las localidades disponibles para la lista desplegable
  Locations: Location[] = [];

  newTheme = this._fb.group({
    tName: ['', [Validators.required, Validators.maxLength(50)]],
    LocationId: ['', [Validators.required]],
    tStatus: ['', [Validators.required]],
  });

  constructor(
    private _fb: FormBuilder,
    private _themeService: ThemeService,
    private _fieldsService: FieldsService,
    private _listService: ListService,
    private _searchService: SearchService,
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
    this.getThemeList();
    this.getLocations();
  }

  // Obtencion de las localidas disponibles
  getLocations(): void {
    this._fieldsService.getLocations().subscribe(
      (data) => {
        this.Locations = [...data];
      },
      (error) => {
        console.error(error.error.message);
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
        },
        (error) => {
          console.error(error.error.message);
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
        console.log(data.message);
        this.getThemeList();
        this.newTheme.reset();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Borrado logico de un tema
  deleteTheme(themeId: number): void {
    this._themeService.deleteTheme(themeId).subscribe(
      (data) => {
        console.log(data.message);
        this.th = null;
        this.getThemeList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Busqueda de un tema en especifico
  search(themeId: string): void {
    if (themeId) {
      // Definicioin de los datos de busquedas
      let themeSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: Number(themeId),
      };

      this._searchService.searchTheme(themeSearch).subscribe(
        (data) => {
          this.th = data;
          this.Theme = [];
        },
        (error) => {
          console.error(error.error.messae);
        }
      );
    }
  }

  // Llamado de modals y actualizacion del tema
  private popup = new Popup();
  themeData: existingTheme;
  editFlag: boolean = false;

  mostrar(themeId: number) {
    this.getExistingTheme(themeId);
    this.popup.mostrar();
  }

  cerrar() {
    this.editFlag = false;
    this.popup.cerrar();
  }

  getExistingTheme(themeId: number): void {
    this._searchService.getExistingTheme(themeId).subscribe(
      (data) => {
        this.themeData = data[0];
        console.log(this.themeData);
        this.editFlag = true;
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  updateTheme = this._fb.group({
    newName: ['', [Validators.maxLength(50)]],
    newStatus: [''],
  });

  saveChanges() {
    const saveChanges: itemChanges = {
      itemId: this.themeData.tId,
      itemName: this.updateTheme.get('newName').value,
      itemStatus: this.updateTheme.get('newStatus').value,
    };

    console.log(saveChanges);

    this._themeService.themeUpdate(saveChanges).subscribe(
      (data) => {
        console.log(data.message);
        this.getThemeList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }
}
