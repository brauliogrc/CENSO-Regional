import { Component, OnInit } from '@angular/core';
import { SquestionsService } from '../services/squestions.service';
import { dataQuestion } from '../interfaces/interfaces';

@Component({
  selector: 'app-preguntas',
  templateUrl: './preguntas.component.html',
  styleUrls: ['./preguntas.component.css']
})
export class PreguntasComponent implements OnInit {

  Questions : dataQuestion[] = [];

  constructor( private _service:SquestionsService) { }

  ngOnInit(): void {
    this.getAllQuestions();
  }

  getAllQuestions(){
    this._service.getQuestions().subscribe(data =>{
      console.log(data);
      this.Questions = [... data];
    }, error => {
      console.error('Error getting data '+ error);
    });
  }
}
