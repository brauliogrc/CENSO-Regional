import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShrUsersService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/RH_Users';

  constructor( private _http:HttpClient ) { }

  
}
