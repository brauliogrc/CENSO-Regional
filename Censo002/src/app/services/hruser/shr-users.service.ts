import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { dataNewUser } from '../../interfaces/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ShrUsersService {
  private MyAppUrl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/RH_Users';

  constructor( private _http:HttpClient ) { }

  getRoles(): Observable<any>{
    return this._http.get(this.MyAppUrl + this.MyApiUrl + '/GetRoles');
  }
  
  addNewUser(dataUser : any) : Observable<any> {
    // console.log('desde el service');
    // console.log(dataUser);
    return this._http.post(this.MyAppUrl + this.MyApiUrl, dataUser);
  }

  deleteUser(id : number) : Observable<any> {
    return this._http.delete( this.MyAppUrl + this.MyApiUrl + '/' + id);
  }

  // getSpecificUser(idUser : any) : Observable<any> {
    
  // }
}
