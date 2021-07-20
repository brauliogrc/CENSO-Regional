import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addAnonRequest } from '../interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class AddAnonRequestService {
  private MyApiUrl: string = 'AnonRequest/';

  constructor(private _http: HttpClient) {}

  // Registro de la pticion en la tabla AnonRequest
  addNewAnonRequest(newAnonRequest: addAnonRequest): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newAnonRequest',
      newAnonRequest
    );
  }

  // Eliminacion de la peticion de la tabla AnonRequest
  deleteAnonRequest(requestId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'deleteAnonRequest/' +
        requestId
    );
  }
}
