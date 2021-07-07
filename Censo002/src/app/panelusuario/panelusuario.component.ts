import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SquestionsService } from '../services/questions/squestions.service';
import { SrequestService } from '../services/request/srequest.service';
import {
  availableQues,
  saveDataLogin,
  newRequest,
  availableTheme,
  availableAreas,
} from '../interfaces/interfaces';
import { SloginService } from '../services/login/slogin.service';
import { FieldsRequestService } from '../services/fieldsRequest/fields-request.service';
import { getLocaleEraNames } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';

@Component({
  selector: 'app-panelusuario',
  templateUrl: './panelusuario.component.html',
  styleUrls: ['./panelusuario.component.css'],
})
export class PanelusuarioComponent implements OnInit {
  // Array que recorreremos desde el html
  Questions: availableQues[] = [];

  Theme: availableTheme[] = [];

  Areas: availableAreas[] = [];

  private location: number = 0;

  private user: saveDataLogin = {
    uId: 0,
    uEmail: '',
    uName: '',
    locationId: 0,
    roleId: 0,
  };

  // Array que almacenará los datos del usuario logeado

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
    private _reqServise: SrequestService,
    private _authService: AuthService,
    private _fields: FieldsRequestService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.user = this._authService.getUser();
    // console.log(this.user);

    this.defineLocation();
  }

  buscarUsuario() {
    this.router.navigate(['fvacio']);
  }

  defineLocation() {
    this.location = this.user.locationId;
    this.getTeme();
    this.getAreas();
  }

  getName() {
    return this.user.uName;
  }

  getTeme() {
    this._fields.getTheme(this.location).subscribe(
      (data) => {
        this.Theme = [...data];
        console.log(this.Theme);
      },
      (error) => {
        console.error(error.error.message);
        this.Theme = [];
      }
    );
  }

  getQuetions(themeId: any) {
    this._fields.getQuestions(themeId).subscribe(
      (data) => {
        this.Questions = [...data];
        console.log(this.Questions);
      },
      (error) => {
        console.error(error.error.message);
        this.Questions = [];
      }
    );
  }

  getAreas() {
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

  registerRequest() {
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const req: newRequest = {
      rUserId: this.user.uId,
      rEmployeeType: this.bodyRequest.get('rEmployeeType')?.value,
      QuestionId: this.bodyRequest.get('QuestionId')?.value,
      AreaId: this.bodyRequest.get('AreaId')?.value,
      rIssue: this.bodyRequest.get('rIssue')?.value,
      rAttachement: this.bodyRequest.get('rAttachement')?.value,
      ThemeId: this.bodyRequest.get('ThemeId')?.value,
      LocationId: this.user.locationId,
    };
    console.log(req);

    // Nos suscribimos al método del service, enviandole el objeto con los datos a registrar en la base de datos
    this._reqServise.saveRequest(req).subscribe(
      (data) => {
        this.bodyRequest.reset();
        console.log(data);
        console.log('Petición registrada con exito. N folio: ' + data.rId);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
