import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addAnswer, searchData } from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  private MyApiUrl: string = 'Answer/';

  constructor(private _http: HttpClient) {}

  // Registro de una nueva respuesta en la tabla Answer
  addNewAnswer(newAnswer:any): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newAnswer',
      newAnswer
    );
  }
}
