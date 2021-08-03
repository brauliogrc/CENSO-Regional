import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { addRequest } from 'src/app/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AddRequestService {
  private MyApiUrl: string = 'Request/';

  constructor(private _http: HttpClient) {}

  // Registro de la peticion en la tabla Request
  addNewRequest(newRequest: any): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newRequest',
      newRequest
    );
  }

  // Eliminacion de la peticion de la tabla Request
  deleteRequest(requestId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteRequest/' + requestId
    );
  }
}
