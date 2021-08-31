import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { itemChanges } from '../../../../assets/ts/interfaces/newInterfaces';
import {
  addLocation,
} from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  private MyApiUrl: string = 'Location/';
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Registro de una nueva localidad en la tabala locations
  addNewLocation(newLocation: addLocation): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newLocation',
      newLocation,
      { headers: this.headers }
    );
  }

  // Borrado logico de una localidad
  deleteLocaion(locationId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteLocation/' + locationId,
      { headers: this.headers }
    );
  }

  // Actualización de la localidad deleccionada
  locatinoUpdate(newLocationData: itemChanges): Observable<any> {
    return this._http.patch(
      `${environment.API_URL}` + this.MyApiUrl + 'locationUpdate',
      newLocationData,
      { headers: this.headers }
    );
  }
}
