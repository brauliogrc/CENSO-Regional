import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SquestionsService } from '../services/questions/squestions.service';
import { SrequestService } from '../services/request/srequest.service';
import { availableQues, newAnonRequest, availableLocations, availableTheme } from '../interfaces/interfaces';
import { DataTableService } from '../services/tables/data-table.service';
import { SthemeService } from '../services/theme/stheme.service';

@Component({
  selector: 'app-folioanonimoindex',
  templateUrl: './folioanonimoindex.component.html',
  styleUrls: ['./folioanonimoindex.component.css']
})
export class FolioanonimoindexComponent implements OnInit {

  // Array que recorreremos desde el html
  questions: availableQues[] = [];

  Locations : availableLocations[] = [];

  Theme : availableTheme[] = [];

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
  */
  bodyRequest = this._fb.group({
    arEmployeeType:  ['', [Validators.required]],
    QuestionId: ['', [Validators.required]],
    AreaId : ['', [Validators.required]],
    arIssue: ['', [Validators.required, Validators.maxLength(500)]],
    arAttachemen: ['', [Validators.maxLength(200)]],
    location: ['', [Validators.required]],
    ThemeId: ['', [Validators.required]]
  });

  constructor(  private _fb:FormBuilder,
                private _reqService:SrequestService,
                private _questionService:SquestionsService,
                private _dataTable:DataTableService,
                private _themService:SthemeService) { 
  }

  ngOnInit(): void {

    this._dataTable.tableLocations().subscribe(data => {
      this.Locations = [... data];
      console.log(this.Locations);
      
    }, error => {
      console.error(error);
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
      ThemeId: this.bodyRequest.get('ThemeId')?.value
    }
    console.log(anonReq);
    
    // Nos suscribimos al método del service, enviandole el objeto con los datos a registrar en la base de datos
    this._reqService.saveAnonRequest(anonReq).subscribe(data => {
      this.bodyRequest.reset();
      console.log(data);
      console.log('Petición registrada con exito. N folio: ' + data.arId);
    }, error => {
      console.error(error);
    })
  }

  getThemes(idLocation : any){
    this._themService.getSpecificsThems(idLocation).subscribe( data => {
      console.log(data);
      this.Theme = [... data];
    }, error => {
      console.error(error);
    })
  }

  // Obtenemos las questions que se encuentran disponibles suscribiendonos al método del service
  getQuestions(idTheme : any){
    
  }
}
