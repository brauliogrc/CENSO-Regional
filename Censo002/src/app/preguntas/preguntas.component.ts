import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { dataQuestion, availableLocations, availableTheme, dataNewQuestion } from '../interfaces/interfaces';
import { SquestionsService } from '../services/questions/squestions.service';
// import { SquestionsService } from '../services/questions/squestions.service';
import { DataTableService } from '../services/tables/data-table.service';
// import { MatSelect } from '@angular/material/select';
import { SthemeService } from '../services/theme/stheme.service';

@Component({
  selector: 'app-preguntas',
  templateUrl: './preguntas.component.html',
  styleUrls: ['./preguntas.component.css']
})
export class PreguntasComponent implements OnInit {

  // Array que contiene loa datos de las preguntas para ser mostrados en la tabla
  questions : dataQuestion [] = [];

  // Arrar que contiene las locations disponibles para mostrar en la lista desplegable
  Locations : availableLocations [] = [];
  
  Theme : availableTheme [] = [];

  newQuestion = this._fb.group({
    Locations: ['', [Validators.required]],
    qName: ['', [Validators.required, Validators.maxLength(70)]],
    ThemeId: ['', [Validators.required]],
    qStatus: ['', [Validators.required]]
  })

  constructor(  private _service:DataTableService,
                private _fb:FormBuilder,
                private _themService:SthemeService,
                private _questionService:SquestionsService) { }

  ngOnInit(): void {
    this.getAllQuestiosn();

    // Obtenemos las Locations dispnibles
    this._service.tableLocations().subscribe(data => {
      this.Locations = [... data];
    }, error => {
      console.error(error);
    })
  }

  getAllQuestiosn(){
    this._service.tableQuestions().subscribe(data =>{
      this.questions = [... data];
    }, error => {
      console.error( 'Error getting data ' + error );
    })
  }

  addNewQuestion(){
    const dataNewQuestion : dataNewQuestion = {
      qName: this.newQuestion.get('qName')?.value,
      qStatus: this.newQuestion.get('qStatus')?.value,
      ThemeId: this.newQuestion.get('ThemeId')?.value
    }
    console.log(dataNewQuestion);
    
    this._questionService.addNewQuestion(dataNewQuestion).subscribe(data => {
      console.log(data);
      alert(`Nueva pregunta ${data.qName} registrada con el id ${data.qId}.`);
      this.getAllQuestiosn();
      this.newQuestion.reset();
    }, error => {
      console.error(error);
    })
  }

  onSelect(id : any) : void {
    this._themService.getSpecificsThems(id).subscribe( data => {
      console.log(data);
      this.Theme = [... data];
    }, error => {
      console.error(error);
    })
  }
}
