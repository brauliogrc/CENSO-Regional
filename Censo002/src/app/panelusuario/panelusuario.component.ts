import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { LocationValidate } from '../services/validations';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { Theme, Question, Area, addRequest } from '../interfaces/newInterfaces';
import { AddRequestService } from '../services/newServices/Request/add-request.service';

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
  });

  constructor(
    private _fb: FormBuilder,
    private _requestService: AddRequestService,
    private _authService: AuthService,
    private _fields: FieldsService,
    private router: Router
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
    this._fields.getThme(this.location).subscribe(
      (data) => {
        this.Theme = [...data];
        console.log(this.Theme);
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtencion de las preguntas relaciondas al tema
  getQuetions(themeId: string): void {
    this._fields.getQuestions(Number(themeId)).subscribe(
      (data) => {
        this.Questions = [...data];
        console.log(this.Questions);
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
        console.log(this.Areas);
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Registro de la pticion en la base de datos
  registerRequest(): void {
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const req: addRequest = {
      rUserId: Number(sessionStorage.getItem('employeeNumber')),
      rUserName: sessionStorage.getItem('username'),
      rEmployeeType: this.bodyRequest.get('rEmployeeType')?.value,
      rEmployeeLeader: Number(sessionStorage.getItem('supervisorNumber')),
      QuestionId: this.bodyRequest.get('QuestionId')?.value,
      AreaId: this.bodyRequest.get('AreaId')?.value,
      ThemeId: this.bodyRequest.get('ThemeId')?.value,
      LocationId: this.location,
      rIssue: this.bodyRequest.get('rIssue')?.value,
      rAttachement: this.bodyRequest.get('rAttachement')?.value,
    };
    console.log(req);

    // Registro de la peticion en la base de datos
    this._requestService.addNewRequest(req).subscribe(
      (data) => {
        console.log(data);
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Llamada el método de cerrar sesion
  logout(): void {
    this._authService.logout();
  }
}
