import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SlocationsService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/Locations';

  constructor( private _http:HttpClient ) { }

  getLocations() : Observable<any>{
    return this._http.get( this.MyAppUrl + this.MyApiUrl );
  }
}
