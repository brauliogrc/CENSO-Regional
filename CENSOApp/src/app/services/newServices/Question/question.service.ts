import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addThemeRelationship } from '../../../../assets/ts/interfaces/newInterfaces';
import {
  addQuestion,
  itemChanges,
} from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  private MyApiUrl: string = 'Question/';
  private headers = new HttpHeaders({
    Authorization: `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Registro de una nueva pregunta en la tabla Questions
  addNewQuestion(newQuetion: addQuestion): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newQuestion/',
      newQuetion,
      { headers: this.headers }
    );
  }

  // Borrado logico de una pregunta
  deleteQuestion(questionId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteQuestion/' + questionId,
      { headers: this.headers }
    );
  }

  // Actualización de la pregunta
  questionUpdate(newQuestionData: itemChanges): Observable<any> {
    return this._http.patch(
      `${environment.API_URL}` + this.MyApiUrl + 'questionUpdate',
      newQuestionData,
      { headers: this.headers }
    );
  }

  // Eliminación de la relación entre una pregunta y un tema
  deleteRelatedTopic(themeId: number, questionId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'deleteRelatedTheme/' +
        themeId +
        '/' +
        questionId,
      { headers: this.headers }
    );
  }

  // Añadir una relación entre la pregunta y el tema
  addRelatedTheme(relationship: addThemeRelationship): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'addRelatedTheme',
      relationship,
      { headers: this.headers }
    );
  }
}
