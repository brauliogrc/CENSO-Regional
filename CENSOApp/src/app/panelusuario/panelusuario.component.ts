import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { LocationValidate } from '../../assets/ts/validations';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import {
  Theme,
  Question,
  Area,
  addRequest,
} from '../../assets/ts/interfaces/newInterfaces';
import { AddRequestService } from '../services/newServices/Request/add-request.service';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-panelusuario',
  templateUrl: './panelusuario.component.html',
  styleUrls: ['./panelusuario.component.css'],
})
export class PanelusuarioComponent implements OnInit {
  // Array que recorreremos desde el html
  Questions: Question[] = [];

  Theme: Theme[] = [];

  Areas: Area[] = [];

  // Id de la localidad
  private location: number = 0;

  public folioSent: boolean = true;

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
   */
  bodyRequest = this._fb.group({
    rEmployeeType: ['', [Validators.required]],
    QuestionId: ['', [Validators.required]],
    AreaId: ['', [Validators.required]],
    rIssue: ['', [Validators.required, Validators.maxLength(500)]],
    rAttachement: ['', [Validators.maxLength(200)]],
    ThemeId: ['', [Validators.required]],
    Terminos: ['', [Validators.required]],
  });

  constructor(
    private router: Router,
    private _fb: FormBuilder,
    private _fields: FieldsService,
    private _authService: AuthService,
    private _showError: ShowErrorService,
    private _requestService: AddRequestService
  ) {}

  ngOnInit(): void {
    // this.user = this._authService.getUser();
    // console.log(this.user);

    this.defineLocation();
  }

  // Redireccion al componente fvacio
  buscarUsuario(): void {
    this.router.navigate(['fvacio']);
  }

  // Definicion del id de la localidad para consultas
  defineLocation(): void {
    const tokenValue: string | null = sessionStorage.getItem('location');
    const locationValidate = new LocationValidate();
    const locationValue = locationValidate.localityValidation(tokenValue);

    if (locationValue == null) {
      this.location = Number(sessionStorage.getItem('location'));
    } else {
      this.location = locationValue;
    }

    this.getTeme();
    this.getAreas();
  }

  // Mostrado del nombre en el formulatio
  getName(): string | null {
    return sessionStorage.getItem('username');
  }

  // Obtencion de los temas relacionados a la localidad
  getTeme(): void {
    this.Theme = [];
    this._fields.getThme(this.location).subscribe(
      (data) => {
        this.Theme = [...data];
        // console.log(this.Theme);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Obtencion de las preguntas relaciondas al tema
  getQuetions(themeId: string): void {
    this.Questions = [];
    this._fields.getQuestions(Number(themeId)).subscribe(
      (data) => {
        this.Questions = [...data];
        // console.log(this.Questions);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
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
        // console.log(this.Areas);
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Registro de la pticion en la base de datos
  registerRequest(): void {
    this.folioSent = false;
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    // const req: addRequest = {
    //   rUserId: Number(sessionStorage.getItem('employeeNumber')),
    //   rUserName: sessionStorage.getItem('username'),
    //   rEmployeeType: this.bodyRequest.get('rEmployeeType')?.value,
    //   rEmployeeLeader: Number(sessionStorage.getItem('supervisorNumber')),
    //   QuestionId: this.bodyRequest.get('QuestionId')?.value,
    //   AreaId: this.bodyRequest.get('AreaId')?.value,
    //   ThemeId: this.bodyRequest.get('ThemeId')?.value,
    //   LocationId: this.location,
    //   rIssue: this.bodyRequest.get('rIssue')?.value,
    //   rAttachement: this.bodyRequest.get('rAttachement')?.value,
    // };
    // console.log(req);

    const formData = new FormData();
    formData.append(
      'rUserId',
      String(sessionStorage.getItem('employeeNumber'))
    );
    formData.append('rUserName', String(sessionStorage.getItem('username')));
    formData.append(
      'rEmployeeType',
      this.bodyRequest.get('rEmployeeType')?.value
    );
    formData.append(
      'rEmployeeLeader',
      String(sessionStorage.getItem('supervisorNumber'))
    );
    formData.append('QuestionId', this.bodyRequest.get('QuestionId')?.value);
    formData.append('AreaId', this.bodyRequest.get('AreaId')?.value);
    formData.append('ThemeId', this.bodyRequest.get('ThemeId')?.value);
    formData.append('LocationId', String(this.location));
    formData.append('rIssue', this.bodyRequest.get('rIssue')?.value);
    formData.append('rAttachement', this.file);
    
    setTimeout( () => {
      this.folioSent = true;
    }, 3000 );

    // Registro de la peticion en la base de datos
    this._requestService.addNewRequest(formData).subscribe(
      (data) => {
        // console.log(data);
        this._showError.success(data.message);
        this.bodyRequest.reset();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
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
      // console.log(file + 'asamclskmx');
    }
  };

  // Llamada el método de cerrar sesion
  logout(): void {
    this._authService.logout();
  }
}
