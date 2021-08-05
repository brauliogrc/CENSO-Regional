import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { addAnonRequest } from 'src/app/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AddAnonRequestService {
  private MyApiUrl: string = 'AnonRequest/';

  // Definicion de los headers
  headers = new HttpHeaders().set(
    'Authorization',
    `${sessionStorage.getItem('token')}`
  );

  constructor(private _http: HttpClient) {}

  // Registro de la pticion en la tabla AnonRequest
  addNewAnonRequest(newAnonRequest: any): Observable<any> {
    return this._http
      .post(
        `${environment.API_URL}` + this.MyApiUrl + 'newAnonRequest',
        newAnonRequest
      )
      .pipe(
        catchError((err: any) => {
          console.warn(err);
          return throwError(err);
        })
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
