import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SquestionsService {
  private MyAppUrl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/Question';

  constructor(private _http: HttpClient) {}

  // Cambir ruta EmpyCensoController
  getQuestions(): Observable<any> {
    return this._http.get(this.MyAppUrl + this.MyApiUrl);
  }

  addNewQuestion(dataQuestion: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.post(this.MyAppUrl + this.MyApiUrl, dataQuestion, {
      headers: headers,
    });
  }

  deleteQuestion(id: number): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.delete(this.MyAppUrl + this.MyApiUrl + '/' + id, {
      headers: headers,
    });
  }
}
