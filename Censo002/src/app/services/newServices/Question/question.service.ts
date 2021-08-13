import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addQuestion } from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class QuestionService {
  private MyApiUrl: string = 'Question/';

  constructor(private _http: HttpClient) {}

  // Registro de una nueva pregunta en la tabla Questions
  addNewQuestion(newQuetion: addQuestion): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newQuestion/',
      newQuetion
    );
  }

  // Borrado logico de una pregunta
  deleteQuestion(questionId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteQuestion/' + questionId
    );
  }
}
