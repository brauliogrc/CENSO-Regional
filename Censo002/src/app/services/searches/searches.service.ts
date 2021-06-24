import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchesService {

  private MyAppurl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/FoliosSearch/';

  constructor(
    private _http:HttpClient
  ) { }

  searchFolio(folioId : any) : Observable<any>{
    return this._http.get( this.MyAppurl + this.MyApiUrl + 'folioSearch/' + folioId );
  }
}
