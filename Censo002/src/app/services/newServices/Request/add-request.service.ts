import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { addRequest } from 'src/assets/ts/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AddRequestService {
  private MyApiUrl: string = 'Request/';

  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Registro de la peticion en la tabla Request
  addNewRequest(newRequest: any): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newRequest',
      newRequest,
      { headers: this.headers }
    );
  }

  // Eliminacion de la peticion de la tabla Request
  deleteRequest(requestId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteRequest/' + requestId,
      { headers: this.headers }
    );
  }
}
