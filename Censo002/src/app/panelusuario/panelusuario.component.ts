import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SquestionsService } from '../services/questions/squestions.service';
import { SrequestService } from '../services/request/srequest.service';
import { availableQues, saveDataLogin } from '../interfaces/interfaces';
import { SloginService } from '../services/login/slogin.service';

@Component({
  selector: 'app-panelusuario',
  templateUrl: './panelusuario.component.html',
  styleUrls: ['./panelusuario.component.css']
})
export class PanelusuarioComponent implements OnInit {

  // Array que recorreremos desde el html
  questions: availableQues[] = [];

  // Array que almacenará los datos del usuario logeado

  /* Definimos los campos del formulario y agregamos validaciones sobre su contenido
   *  Campo en el Form tiene una propiedad "formControlName" que debe coincidir el nombre de las variables a continuación
  */
  bodyRequest = this._fb.group({
    //rUserId: ['', [Validators.required]],
    rEmployeeType: ['', [Validators.required]],
    QuestionId: ['',[Validators.required]],
    AreaId: ['', [Validators.required]],
    rIssue: ['', [Validators.required, Validators.maxLength(500)]],
    rAttachement: ['', [Validators.maxLength(200)]]
  });

  constructor(  private _fb:FormBuilder,
                private _reqServise:SrequestService,
                private _questionService:SquestionsService,
                private _logService:SloginService
                ) { }

  ngOnInit(): void {
    // Obtenemos las questions que se encuentran disponibles suscribiendonos al método del service
    this._questionService.getQuestions().subscribe(data => {
      console.log(data);
      this.questions = [... data];
    }, error => {
      console.error(error);
    });
  }

  registerRequest(){
    /**
     * Obtenermos el valor de cada uno de los campos del Form y lo asignamos a un objeto
     */
    const req : any = { // asignar a una interface
      rEmployeeType: this.bodyRequest.get('rEmployeeType')?.value,
      QuestionId: this.bodyRequest.get('QuestionId')?.value,
      AreaId: this.bodyRequest.get('AreaId')?.value,
      rIssue: this.bodyRequest.get('rIssue')?.value,
      rAttachement: this.bodyRequest.get('rAttachement')?.value
    }
    console.log(req);
    
    // Nos suscribimos al método del service, enviandole el objeto con los datos a registrar en la base de datos
    this._reqServise.saveRequest(req).subscribe(data => {
      this.bodyRequest.reset();
      console.log(data);
      console.log('Petición registrada con exito. N folio: ' + data.rId);
    }, error => {
      console.error(error);
      
    })
  }

}

