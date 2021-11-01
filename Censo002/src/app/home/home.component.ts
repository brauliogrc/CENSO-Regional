import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  Location,
  Theme,
  Question,
  Area,
  addAnonRequest,
} from '../../assets/ts/interfaces/newInterfaces';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { AddAnonRequestService } from '../services/newServices/AnonRequest/add-anon-request.service';
import { TicketService } from '../services/newServices/Ticket/ticket.service';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { App2 } from '../../assets/ts/app2';
import { environment } from 'src/environments/environment.prod';

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
    Terminos: ['', [Validators.required]],
  });

  constructor(
    private _fb: FormBuilder,
    private _fields: FieldsService,
    private _ticketService: TicketService,
    private _anonRequestService: AddAnonRequestService,
    private _showError: ShowErrorService,
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
      (error: HttpErrorResponse) => {
        console.error(error);
        this._showError.statusCode(error);
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
    this.Theme = [];
    this._fields.getThme(this.location).subscribe(
      (data) => {
        this.Theme = [...data];
      },
      (error: HttpErrorResponse) => {
        console.error(error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtencion de las preguntas relacionadas a los temas
  getQuestions(themeId: string): void {
    this.Questions = [];
    this._fields.getQuestions(Number(themeId)).subscribe(
      (data) => {
        this.Questions = [...data];
      },
      (error: HttpErrorResponse) => {
        console.error(error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtencion de las areas relacionadas a la localidad
  getAreas(): void {
    this.Areas = [];
    this._fields.getAreas(this.location).subscribe(
      (data) => {
        this.Areas = [...data];
      },
      (error: HttpErrorResponse) => {
        console.error(error);
        this._showError.statusCode(error);
      }
    );
  }

  // Registro de la peticion en la base de datos
  registerAnonRequest(): void {
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    //  const anonReq: addAnonRequest = {
    //   arEmployeeType: this.bodyRequest.get('arEmployeeType')?.value,
    //   QuestionId: this.bodyRequest.get('QuestionId')?.value,
    //   AreaId: this.bodyRequest.get('AreaId')?.value,
    //   ThemeId: this.bodyRequest.get('ThemeId')?.value,
    //   LocationId: this.bodyRequest.get('LocationId')?.value,
    //   arIssue: this.bodyRequest.get('arIssue')?.value,
    //   arAttachemen: this.file,
    // };

    const formData = new FormData();
    formData.append(
      'arEmployeeType',
      this.bodyRequest.get('arEmployeeType')?.value
    );
    formData.append('QuestionId', this.bodyRequest.get('QuestionId')?.value);
    formData.append('AreaId', this.bodyRequest.get('AreaId')?.value);
    formData.append('ThemeId', this.bodyRequest.get('ThemeId')?.value);
    formData.append('LocationId', this.bodyRequest.get('LocationId')?.value);
    formData.append('arIssue', this.bodyRequest.get('arIssue')?.value);
    formData.append('arAttachement', this.file);
    

    // Registro de la peticion anonima en la base de datos
    this._anonRequestService.addNewAnonRequest(formData).subscribe(
      (data) => {
        // console.log(data);
        this._showError.success(data.message);
        this.bodyRequest.reset();
        let fileName = data[0];
      },
      (error: HttpErrorResponse) => {
        console.error(error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Upload selected file
  private file: any;

  onFileSelected = (event: any) => {
    const file: File = event.target.files[0];

    if (file) {
      this.file = file;

      console.log(file);
    }
  };

  // ==================================================================================================================================================

  // PANEL DE BUSQUEDA

  folio: any;
  responsable: any;

  private app = new App2();
  answerFlag: boolean = false;

  // Busqueda de folio en la base de datos con base en su id
  searchFolio(folioId: any) {
    this._ticketService.getAnonTicketStatus(Number(folioId)).subscribe(
      (data) => {
        this.folio = data.anonTicket[0];
        console.log(this.folio);
        if (data.answer) {
          this.responsable = data.answer[0];
          this.responsable.asAttachement = environment.FileRoute + this.responsable.asAttachement;
          this.answerFlag = true;
          console.log(this.responsable);
        } else {
          console.log(data.message);
          this._showError.success(data.message);
        }
        this.app.mostrarbusq();
      },
      (error: HttpErrorResponse) => {
        console.error(error.message);
        this._showError.statusCode(error);
      }
    );
  }

  cerrarPopup(): void {
    this.app.cerrarbusq();
    this.answerFlag = false;
  }
}
