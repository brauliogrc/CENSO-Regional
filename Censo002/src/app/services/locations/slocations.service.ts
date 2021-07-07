import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SlocationsService {
  private MyAppUrl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/Locations';

  constructor(private _http: HttpClient) {}

  addNewLocation(dataLocation: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    console.log('desde el service');
    console.log(dataLocation);
    return this._http.post(this.MyAppUrl + this.MyApiUrl, dataLocation, {
      headers: headers,
    });
  }

  deleteLocation(id: number): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    console.log('Se elimnara la localidad con el id ' + id);
    return this._http.delete(this.MyAppUrl + this.MyApiUrl + '/' + id, {
      headers: headers,
    });
  }
}
