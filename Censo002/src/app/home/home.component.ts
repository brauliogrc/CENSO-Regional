import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SearchesService } from '../services/searches/searches.service';
import { Router } from '@angular/router';
import {
  Location,
  Theme,
  Question,
  Area,
  addAnonRequest,
} from '../interfaces/newInterfaces';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { AddAnonRequestService } from '../services/newServices/AnonRequest/add-anon-request.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  // Contenedor de los datos de las localidades
  Locations: Location[] = [];

  // Contenedor de los datos de los temas
  Theme: Theme[] = [];

  // Contenedor de los datos de las preguntas
  Questions: Question[] = [];

  // Contenedor de los datos de las areas
  Areas: Area[] = [];

  // Id de la localidad
  private location: number = 0;

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
   */
  bodyRequest = this._fb.group({
    arEmployeeType: ['', [Validators.required]],
    QuestionId: ['', [Validators.required]],
    AreaId: ['', [Validators.required]],
    arIssue: ['', [Validators.required, Validators.maxLength(500)]],
    arAttachemen: ['', [Validators.maxLength(200)]],
    LocationId: ['', [Validators.required]],
    ThemeId: ['', [Validators.required]],
  });

  constructor(
    private _fb: FormBuilder,
    private _fields: FieldsService,
    private _anonRequestService: AddAnonRequestService,
    private _searchFolio: SearchesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getLocations();
  }

  // Obtencion de la localidades disponibles
  getLocations(): void {
    this._fields.getLocations().subscribe(
      (data) => {
        this.Locations = [...data];
        console.log(this.Locations);
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Redireccionamiento al login
  navegarLogin(): void {
    this.router.navigate(['/login']);
  }

  // Establecemos el id de la localidad y llamamos a los métodos que dependen de él
  defineLocation(location: any): void {
    this.location = location;
    this.getTheme();
    this.getAreas();
  }

  // Obtencion de los temas relacionados a la localidad
  getTheme(): void {
    this._fields.getThme(this.location).subscribe(
      (data) => {
        this.Theme = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtencion de las preguntas relacionadas a los temas
  getQuestions(themeId: string): void {
    this._fields.getQuestions(Number(themeId)).subscribe(
      (data) => {
        this.Questions = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtencion de las areas relacionadas a la localidad
  getAreas(): void {
    this._fields.getAreas(this.location).subscribe(
      (data) => {
        this.Areas = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Registro de la peticion en la base de datos
  registerAnonRequest(): void {
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const anonReq: addAnonRequest = {
      arEmployeeType: this.bodyRequest.get('arEmployeeType')?.value,
      QuestionId: this.bodyRequest.get('QuestionId')?.value,
      AreaId: this.bodyRequest.get('AreaId')?.value,
      ThemeId: this.bodyRequest.get('ThemeId')?.value,
      LocationId: this.bodyRequest.get('LocationId')?.value,
      arIssue: this.bodyRequest.get('arIssue')?.value,
      arAttachemen: this.bodyRequest.get('arAttachemen')?.value,
    };
    console.log(anonReq);

    // Registro de la peticion anonima en la base de datos
    this._anonRequestService.addNewAnonRequest(anonReq).subscribe(
      (data) => {
        console.log(data);
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // ==================================================================================================================================================

  // PANEL DE BUSQUEDA

  folio: any;

  // Busqueda de folio en la base de datos con base en su id
  searchFolio(folioId: any) {
    this._searchFolio.searchFolioAnon(folioId).subscribe(
      (data) => {
        this.folio = data[0];
        console.log(this.folio);
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }
}
