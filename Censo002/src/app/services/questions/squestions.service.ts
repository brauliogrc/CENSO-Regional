import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SquestionsService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/Question';

  constructor( private _http:HttpClient) { }

  getQuestions() : Observable<any>{
    return this._http.get( this.MyAppUrl + this.MyApiUrl );
  }
}
