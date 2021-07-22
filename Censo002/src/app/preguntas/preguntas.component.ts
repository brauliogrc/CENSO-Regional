import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';

@Component({
  selector: 'app-preguntas',
  templateUrl: './preguntas.component.html',
  styleUrls: ['./preguntas.component.css'],
})
export class PreguntasComponent implements OnInit {
  // Array que contiene loa datos de las preguntas para ser mostrados en la tabla
  questions: any[] = [];

  question: any;

  // Array que contiene las locations disponibles para mostrar en la lista desplegable
  Locations: any[] = [];

  Theme: any[] = [];

  newQuestion = this._fb.group({
    Locations: ['', [Validators.required]],
    qName: ['', [Validators.required, Validators.maxLength(70)]],
    ThemeId: ['', [Validators.required]],
    qStatus: ['', [Validators.required]],
  });

  constructor(
    private _fb: FormBuilder,
    private _authService: AuthService,
    private router: Router
  ) {}

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
    this.getAllQuestiosn();
    this.getLocations();
  }

  getLocations() {
    // Obtenemos las Locations dispnibles
    // this._fields.getLocations().subscribe(
    //   (data) => {
    //     this.Locations = [...data];
    //   },
    //   (error) => {
    //     console.error(error);
    //   }
    // );
  }

  getAllQuestiosn() {
    // this._service.tableQuestions().subscribe(
    //   (data) => {
    //     this.questions = [...data];
    //   },
    //   (error) => {
    //     console.error('Error getting data ' + error);
    //   }
    // );
  }

  addNewQuestion() {
    const dataNewQuestion: any = {
      qName: this.newQuestion.get('qName')?.value,
      qStatus: this.newQuestion.get('qStatus')?.value,
      ThemeId: this.newQuestion.get('ThemeId')?.value,
    };
    console.log(dataNewQuestion);

    // this._questionService.addNewQuestion(dataNewQuestion).subscribe(
    //   (data) => {
    //     console.log(data);
    //     alert(`Nueva pregunta ${data.qName} registrada con el id ${data.qId}.`);
    //     this.getAllQuestiosn();
    //     this.newQuestion.reset();
    //   },
    //   (error) => {
    //     console.error(error);
    //   }
    // );
  }

  onSelect(id: any): void {
    // this._fields.getTheme(id).subscribe(
    //   (data) => {
    //     console.log(data);
    //     this.Theme = [...data];
    //   },
    //   (error) => {
    //     console.error(error);
    //   }
    // );
  }

  deleteQuestion(id: number) {
    // this._questionService.deleteQuestion(id).subscribe(
    //   (data) => {
    //     console.log('Pregunta eliminada');
    //     alert(`Pregunta "${data.qName}" eliminada`);
    //     this.getAllQuestiosn();
    //   },
    //   (error) => {
    //     console.error(error);
    //   }
    // );
  }

  search(idQuestion: any) {
    // this._searches.getSpecificQuestion(idQuestion).subscribe(
    //   (data) => {
    //     this.question = data;
    //     this.questions = [];
    //     console.log(this.question);
    //   },
    //   (error) => {
    //     alert(error);
    //   }
    // );
  }
}
