import { Component, OnInit } from '@angular/core';
import { dataQuestion } from '../interfaces/interfaces';
// import { SquestionsService } from '../services/questions/squestions.service';
import { DataTableService } from '../services/tables/data-table.service';

@Component({
  selector: 'app-preguntas',
  templateUrl: './preguntas.component.html',
  styleUrls: ['./preguntas.component.css']
})
export class PreguntasComponent implements OnInit {
  questions : dataQuestion [] = [];

  constructor( private _service:DataTableService ) { }

  ngOnInit(): void {
    this.getAllQuestiosn();
  }

  getAllQuestiosn(){
    this._service.tableQuestions().subscribe(data =>{
      this.questions = [... data];
    }, error => {
      console.error( 'Error getting data ' + error );
    })
  }

}
