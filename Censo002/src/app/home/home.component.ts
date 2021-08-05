import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
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
import { TicketService } from '../services/newServices/Ticket/ticket.service';
import { statusCode } from '../../assets/ts/SweetAlert';

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
    this.Theme = [];
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
    this.Questions = [];
    this._fields.getQuestions(Number(themeId)).subscribe(
      (data) => {
        this.Questions = [...data];
      },
      (error) => {
        console.error(Number(error.status), 'error code');
        statusCode(Number(error.status), error.error.message);
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
        console.log(data);
        this.bodyRequest.reset();
        let fileName = data[0];
      },
      (error) => {
        console.error(error.error.message);
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

  // Busqueda de folio en la base de datos con base en su id
  searchFolio(folioId: any) {
    this._ticketService.getAnonTicketStatus(Number(folioId)).subscribe(
      (data) => {
        this.folio = data.anonTicket[0];
        console.log(this.folio);
        if (data.answer) {
          this.responsable = data.answer;
          console.log(this.responsable);
        } else {
          console.log(data.message);
        }
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }
}
