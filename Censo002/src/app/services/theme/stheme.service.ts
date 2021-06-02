import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SthemeService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/Theme';

  constructor( private _http:HttpClient) { }

  getTheme() : Observable<any>{
    return this._http.get( this.MyAppUrl + this.MyApiUrl );
  }
}
