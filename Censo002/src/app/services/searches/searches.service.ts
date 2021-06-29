import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchesService {

  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/SpecificSearches/';

  constructor(
    private _http:HttpClient
  ) { }

  // Llamada al método de la api para buscar el folio en la base de datos
  searchFolio(folioId : any) : Observable<any>{
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'folioSearch/' + folioId );
  }

  getSpecificLocation(idLocation : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'LocationSearch/' + idLocation);
  } 

  getSpecificUser(idUser : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'UserSearch/' + idUser);
  }

  getSpecificTheme(idTheme : any ) : Observable<any> { 
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'ThemeSearch/' + idTheme);
  }

  getSpecificQuestion(idQuestion : any) : Observable<any> {
    return this._http.get( this.MyAppUrl + this.MyApiUrl + 'QuestionSearch/' + idQuestion);
  }
}
