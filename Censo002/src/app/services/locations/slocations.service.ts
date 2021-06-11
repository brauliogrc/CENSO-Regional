import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SlocationsService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/Locations';
  
  constructor( private _http:HttpClient) { }

  
}
