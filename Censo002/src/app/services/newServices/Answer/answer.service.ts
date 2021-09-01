import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  private MyApiUrl: string = 'Answer/';
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Registro de una nueva respuesta en la tabla Answer
  addNewAnswer(newAnswer: any): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newAnswer',
      newAnswer,
      { headers: this.headers }
    );
  }
}
