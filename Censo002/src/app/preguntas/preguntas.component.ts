import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import {
  questionList,
  Location,
  Theme,
  addQuestion,
} from '../interfaces/newInterfaces';
import { SearchService } from '../services/newServices/Search/search.service';
import { QuestionService } from '../services/newServices/Question/question.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { ListService } from '../services/newServices/List/list.service';
import { searchData } from '../interfaces/newInterfaces';
import { CreateScriptsService } from '../services/newServices/CreateScripts/create-scripts.service';

@Component({
  selector: 'app-preguntas',
  templateUrl: './preguntas.component.html',
  styleUrls: ['./preguntas.component.css'],
})
export class PreguntasComponent implements OnInit {
  // Array que contiene loa datos de las preguntas para ser mostrados en la tabla
  Questions: questionList[] = [];

  question: any;

  // Array que contiene las locations disponibles para mostrar en la lista desplegable
  Locations: Location[] = [];

  Theme: Theme[] = [];

  newQuestion = this._fb.group({
    Locations: ['', [Validators.required]],
    qName: ['', [Validators.required, Validators.maxLength(70)]],
    ThemeId: ['', [Validators.required]],
    qStatus: ['', [Validators.required]],
  });

  constructor(
    private _createScripts: CreateScriptsService,
    private _fb: FormBuilder,
    private _searchService: SearchService,
    private _questionService: QuestionService,
    private _fieldsService: FieldsService,
    private _listService: ListService,
    private _authService: AuthService,
    private router: Router
  ) {
    this._createScripts.CargaArchivos( ["popoupEdicion"] );
  }

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2
    ) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getQuestionList();
    this.getLocations();
  }

  // Obtencion de las localidades disponibles
  getLocations(): void {
    this._fieldsService.getLocations().subscribe(
      (data) => {
        this.Locations = [...data];
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtencion de los temas disponibles segun la localidad
  getTheme(locationId: string): void {
    this.Theme = [];
    if (locationId) {
      this._fieldsService.getThme(Number(locationId)).subscribe(
        (data) => {
          this.Theme = [...data];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
    }
  }

  // Obtencion de las preguntas disponibles
  getQuestionList(): void {
    this._listService
      .getQuestionList(Number(sessionStorage.getItem('location')))
      .subscribe(
        (data) => {
          this.Questions = [...data];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
  }

  // Regustro de una nueva pregunta en la tabla Questions
  addNewQuestion(): void {
    const dataNewQuestion: addQuestion = {
      qName: this.newQuestion.get('qName')?.value,
      qStatus: this.newQuestion.get('qStatus')?.value,
      qCreationUser: Number(sessionStorage.getItem('userId')),
      ThemeId: this.newQuestion.get('ThemeId')?.value,
    };
    console.log(dataNewQuestion);

    this._questionService.addNewQuestion(dataNewQuestion).subscribe(
      (data) => {
        console.log(data.message);
        this.getQuestionList();
        this.newQuestion.reset();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Borrado logico de una pregunta
  deleteQuestion(questionId: number) {
    this._questionService.deleteQuestion(questionId).subscribe(
      (data) => {
        console.log(data.message);
        this.question = null;
        this.getQuestionList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Busqueda de una pregunta en especifico
  search(questionId: any) {
    if (questionId) {
      // Definicion de datos de busqueda
      let questionSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: Number(questionId),
      };

      this._searchService.searchQuestion(questionSearch).subscribe(
        (data) => {
          this.question = data;
          this.Questions = [];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
    }
  }
}
