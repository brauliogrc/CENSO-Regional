import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addLocation, locationList } from '../../../interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  private MyApiUrl: string = 'Location/';

  constructor(private _http: HttpClient) {}

  // Registro de una nueva localidad en la tabala locations
  addNewLocation(newLocation: addLocation): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newLocation',
      newLocation
    );
  }

  // Borrado logico de una localidad
  deleteLocaion(locationId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteLocation/' + locationId
    );
  }
}
