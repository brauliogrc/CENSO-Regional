import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SquestionsService } from '../services/questions/squestions.service';
import { SrequestService } from '../services/request/srequest.service';
import { availableQues } from '../interfaces/interfaces';

@Component({
  selector: 'app-folioanonimoindex',
  templateUrl: './folioanonimoindex.component.html',
  styleUrls: ['./folioanonimoindex.component.css']
})
export class FolioanonimoindexComponent implements OnInit {

  // Array que recorreremos desde el html
  questions: availableQues[] = [];

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
  */
  bodyRequest = this._fb.group({
    arEmployeeType:  ['', [Validators.required]],
    QuestionId: ['', [Validators.required]],
    AreaId : ['', [Validators.required]],
    arIssue: ['', [Validators.required, Validators.maxLength(500)]],
    arAttachemen: ['', [Validators.maxLength(200)]],
  });

  constructor(  private _fb:FormBuilder,
                private _reqService:SrequestService,
                private _questionService:SquestionsService) { 
  }

  ngOnInit(): void {
    // Obtenemos las questions que se encuentran disponibles suscribiendonos al método del service
    this._questionService.getQuestions().subscribe(data => {
      console.log(data);
      this.questions = [... data];
    }, error => {
      console.error(error);
    })
  }

  registerAnonRequest(){
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const anonReq: any = { // Asignar a una interface
      arEmployeeType: this.bodyRequest.get('arEmployeeType')?.value,
      QuestionId: this.bodyRequest.get('QuestionId')?.value,
      AreaId: this.bodyRequest.get('AreaId')?.value,
      arIssue: this.bodyRequest.get('arIssue')?.value,
      arAttachemen: this.bodyRequest.get('arAttachemen')?.value
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

}
