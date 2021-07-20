import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataTableService {
  private MyAppurl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/DataTables/';

  constructor(private _http: HttpClient) {}

  // Llamamos al controlador de la API en la ruta TableLocations para obtener los datos necesarios para la tabla del localidades.component
  tableLocations(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(this.MyAppurl + this.MyApiUrl + 'TableLocations', {
      headers: headers,
    });
  }

  // Llamamos al controlador de la API en la ruta TableUsers para obtener los datos necesarios para la tabla del usuarios.component
  tableUsers(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(this.MyAppurl + this.MyApiUrl + 'TableUsers', {
      headers: headers,
    });
  }

  // Llamamos al controlador de la API en la ruta TableTheme para obtener los datos necesarios para la tabla del temas.component
  tableTheme(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(this.MyAppurl + this.MyApiUrl + 'TableTheme', {
      headers: headers,
    });
  }

  // Llamamos al controlador de la API en la ruta TableQuestions para obtener los datos necesarios para la tabla del preguntas.component
  tableQuestions(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(this.MyAppurl + this.MyApiUrl + 'TableQuestions', {
      headers: headers,
    });
  }

  // Llamamos al controlador de la API en la ruta TableAreas para obtener los datos necesarios para la tabla del areas.component
  tableAreas(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(this.MyAppurl + this.MyApiUrl + 'TableAreas', {
      headers: headers,
    });
  }

  // Llamamos al controlados de la API en la ruta TableTikets para obtener los datos necesarios para la tabla de tickets.component
  tableTickets(locationId: number): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppurl + this.MyApiUrl + 'TableTikets/' + locationId,
      {
        headers: headers,
      }
    );
  }
}
