import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { addTheme, themeList, Location } from '../interfaces/newInterfaces';
import { ThemeService } from '../services/newServices/Theme/theme.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { ListService } from '../services/newServices/List/list.service';

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

  // Obtencion de las localidades disponibles
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
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  deleteTheme(themeId: number): void {
    this._themeService.deleteTheme(themeId).subscribe(
      (data) => {
        console.log(data.message);
        this.getThemeList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  search(idTheme: any) {
    // this._searches.getSpecificTheme(idTheme).subscribe(
    //   (data) => {
    //     this.th = data;
    //     this.theme = [];
    //     console.log(this.th);
    //   },
    //   (error) => {
    //     alert(error.error.message);
    //   }
    // );
  }
}
