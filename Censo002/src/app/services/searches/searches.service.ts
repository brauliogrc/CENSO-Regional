import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SearchesService {
  private MyAppUrl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/SpecificSearches/';

  constructor(private _http: HttpClient) {}

  // Llamada al método de la api para buscar el folio en la base de datos
  searchFolio(folioId: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + 'folioSearch/' + folioId,
      { headers: headers }
    );
  }

  searchFolioAnon(folioId: any): Observable<any> {
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + 'folioAnonSearch/' + folioId
    );
  }

  getSpecificLocation(idLocation: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + 'LocationSearch/' + idLocation,
      { headers: headers }
    );
  }

  getSpecificUser(idUser: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + 'UserSearch/' + idUser,
      { headers: headers }
    );
  }

  getSpecificTheme(idTheme: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + 'ThemeSearch/' + idTheme,
      { headers: headers }
    );
  }

  getSpecificQuestion(idQuestion: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + 'QuestionSearch/' + idQuestion,
      { headers: headers }
    );
  }
}
