import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDataLog, UserResponse } from '../../interfaces/interfaces';

@Injectable({
  providedIn: 'root'
})
export class SloginService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl  : string = 'api/Login';

  constructor( private _http:HttpClient ) { }

  Login(ahutData : any){}
}
