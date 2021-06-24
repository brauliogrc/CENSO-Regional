import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FieldsRequestService } from '../services/fieldsRequest/fields-request.service';
import { availableLocations, newAnonRequest, availableTheme, availableQues, availableAreas } from '../interfaces/interfaces';
import { SrequestService } from '../services/request/srequest.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  locationId : number = 0;
  // Contenedor de los datos de las localidades
  Locations : availableLocations[] = [];

  // Contenedor de los datos de los temas
  Theme : availableTheme[] = [];

  // Contenedor de los datos de las preguntas
  Questions : availableQues[] = [];

  // Contenedor de los datos de las areas
  Areas : availableAreas[] = [];

  constructor(
    private _fb:FormBuilder,
    private _fields:FieldsRequestService,
    private _reqService:SrequestService
  ) { }

  
  ngOnInit(): void {

    // Obtenemos las localidades disponibles
    this._fields.getLocations().subscribe( data => {
      this.Locations = [... data];
      console.log(this.Locations);
    }, error => {
      console.error(error);
    })
  }

  // Establecemos el id de la localidad y llamamos a los métodos que dependen de él
  defineLocation(location : any){
    this.locationId = location;
    this.getTheme();
    this.getAreas();
  }

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
  */
  bodyRequest = this._fb.group({
    arEmployeeType:  ['', [Validators.required]],
    QuestionId: ['', [Validators.required]],
    AreaId : ['', [Validators.required]],
    arIssue: ['', [Validators.required, Validators.maxLength(500)]],
    arAttachemen: ['', [Validators.maxLength(200)]],
    LocationId: ['', [Validators.required]],
    ThemeId: ['', [Validators.required]]
  });

  getTheme(){
    this._fields.getTheme(this.locationId).subscribe( data => {
      this.Theme = [... data];
      console.log(this.Theme);
    }, error => {
      console.error(error.error.message);
      this.Theme =[];
    })
  }

  getQuestions(themeId : any){
    this._fields.getQuestions(themeId).subscribe( data => {
      this.Questions = [... data];
      console.log(this.Questions);
    }, error => {
      console.error(error.error.message);
    })
  }

  getAreas(){
    this._fields.getAreas(this.locationId).subscribe( data => {
      this.Areas = [... data];
      console.log(this.Areas);
    }, error => {
      console.error(error.error.message);
    })
  }

  registerAnonRequest(){
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
     const anonReq: newAnonRequest = {
      arEmployeeType: this.bodyRequest.get('arEmployeeType')?.value,
      QuestionId: this.bodyRequest.get('QuestionId')?.value,
      AreaId: this.bodyRequest.get('AreaId')?.value,
      arIssue: this.bodyRequest.get('arIssue')?.value,
      arAttachemen: this.bodyRequest.get('arAttachemen')?.value,
      ThemeId: this.bodyRequest.get('ThemeId')?.value,
      LocationId: this.bodyRequest.get('LocationId')?.value
    }

    console.log(anonReq);

    // Nos suscribimos al método del service, enviandole el objeto con los datos a registrar en la base de datos
    this._reqService.saveAnonRequest(anonReq).subscribe( data => {
      this.bodyRequest.reset();
      console.log(data);
      alert(`Peticion registrada con exito. N folio: ${data.arId}`);
    }, error => {
      console.error(error);
    })
  }
}

