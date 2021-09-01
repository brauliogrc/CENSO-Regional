import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import {
  questionList,
  Location,
  Theme,
  addQuestion,
  searchData,
  existingQuestion,
} from '../../assets/ts/interfaces/newInterfaces';
import { SearchService } from '../services/newServices/Search/search.service';
import { QuestionService } from '../services/newServices/Question/question.service';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { ListService } from '../services/newServices/List/list.service';
import { Popup } from 'src/assets/ts/popup';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';
import {
  itemChanges,
  addThemeRelationship,
} from '../../assets/ts/interfaces/newInterfaces';

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
    // private _createScripts: CreateScriptsService,
    private router: Router,
    private _fb: FormBuilder,
    private _listService: ListService,
    private _authService: AuthService,
    private _showError: ShowErrorService,
    private _fieldsService: FieldsService,
    private _searchService: SearchService,
    private _questionService: QuestionService
  ) {
    // this._createScripts.CargaArchivos( ["popoupEdicion"] );
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
      this._showError.NotAccessible();
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
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
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
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
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
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
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
        this._showError.success(data.message);
        this.getQuestionList();
        this.newQuestion.reset();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Borrado logico de una pregunta
  deleteQuestion(questionId: number) {
    this._questionService.deleteQuestion(questionId).subscribe(
      (data) => {
        console.log(data.message);
        this._showError.success(data.message);
        this.question = null;
        this.getQuestionList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
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
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
    }
  }

  // Llamado de modals y actualización de preguntas
  private popup = new Popup();
  questionData: existingQuestion;
  relatedTheme: Theme[];
  editFlag: boolean = false;

  mostrar(questionId: number) {
    this.getExistingQuestion(questionId);
    this.popup.mostrar();
  }
  cerrar() {
    this.editFlag = false;
    this.updateQuestion.reset();
    this.popup.cerrar();
  }
  mostrarTema(questionId: number) {
    this.getTheme(sessionStorage.getItem('location'));
    this.getExistingQuestion(questionId);
    this.getRelatedTopics(questionId);
    this.popup.mostrarTema();
  }
  cerrarTema() {
    this.popup.cerrarTema();
  }

  // Obtención de los datos a modificar de la pregunta
  getExistingQuestion(questionId: number): void {
    this._searchService.getExistingQuestion(questionId).subscribe(
      (data) => {
        this.questionData = data[0];
        console.log(this.questionData);
        this.editFlag = true;
      },
      (error: HttpErrorResponse) => {
        this._showError.statusCode(error);
      }
    );
  }

  updateQuestion = this._fb.group({
    newName: [''],
    newStatus: [''],
  });

  // Guardado de los cambios de la pregunta
  saveChanges(): void {
    const saveChanges: itemChanges = {
      modificationUser: Number(sessionStorage.getItem('employeeNumber')),
      itemId: this.questionData.qId,
      itemName: this.updateQuestion.get('newName').value,
      itemStatus: this.updateQuestion.get('newStatus').value,
      locationId: null
    };

    console.log(saveChanges);

    this._questionService.questionUpdate(saveChanges).subscribe(
      (data) => {
        console.log(data.message);
        this._showError.success(data.message);
        this.updateQuestion.reset();
        this.getQuestionList();
      },
      (error: HttpErrorResponse) => {
        this._showError.statusCode(error);
      }
    );
  }

  // Obtención de los temas con los que se relaciona la pregunta
  getRelatedTopics(questionId: number): void {
    this._searchService.getRelatedTopicsQ(questionId).subscribe(
      (data) => {
        this.relatedTheme = [...data];
        console.log(this.relatedTheme);
      },
      (error: HttpErrorResponse) => {
        this._showError.statusCode(error);
      }
    );
  }

  // Añaddir una relación entre la pregunta y el tema
  addRelatedTheme(themeId: number): void {
    const newRelationship: addThemeRelationship = {
      itemId: this.questionData.qId,
      themeId: themeId,
    };

    this._questionService.addRelatedTheme(newRelationship).subscribe(
      (data) => {
        this._showError.success(data.message);
        this.getRelatedTopics(this.questionData.qId);
      },
      (error: HttpErrorResponse) => {
        this._showError.statusCode(error);
      }
    );
  }

  // Obtención del index del tema a eliminar
  getIdx(themeId: number): void {
    this.relatedTheme.find((item, idx) => {
      if (item.tId === themeId) {
        this.deleteRelatedTopic(themeId);
      }
    });
  }

  // Borrado de una relación entre la pregunta y el tema
  deleteRelatedTopic(themeId: number): void {
    this._questionService
      .deleteRelatedTopic(themeId, this.questionData.qId)
      .subscribe(
        (data) => {
          this._showError.success(data.message);
          this.getRelatedTopics(this.questionData.qId);
        },
        (error: HttpErrorResponse) => {
          this._showError.statusCode(error);
        }
      );
  }
}
