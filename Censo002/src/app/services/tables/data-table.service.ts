import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class DataTableService {
  private MyAppurl : string = 'https://localhost:44358/';
  private MyApiUrl : string = 'api/DataTables/';

  constructor( private _http:HttpClient ) { }

  // Llamamos al controlador de la API en la ruta TableLocations para obtener los datos necesarios para la tabla del localidades.component
  tableLocations(): Observable<any>{
    return this._http.get( this.MyAppurl + this.MyApiUrl + 'TableLocations' );
  }

  // Llamamos al controlador de la API en la ruta TableUsers para obtener los datos necesarios para la tabla del usuarios.component
  tableUsers(): Observable<any>{
    return this._http.get( this.MyAppurl + this.MyApiUrl + 'TableUsers' );
  }

  // Llamamos al controlador de la API en la ruta TableTheme para obtener los datos necesarios para la tabla del temas.component
  tableTheme(): Observable<any>{
    return this._http.get( this.MyAppurl + this.MyApiUrl + 'TableTheme' );
  }

  // Llamamos al controlador de la API en la ruta TableQuestions para obtener los datos necesarios para la tabla del preguntas.component
  tableQuestions(): Observable<any>{
    return this._http.get( this.MyAppurl + this.MyApiUrl + 'TableQuestions' );
  }

  // Llamamos al controlador de la API en la ruta TableAreas para obtener los datos necesarios para la tabla del areas.component
  tableAreas(): Observable<any> {
    return this._http.get(this.MyAppurl + this.MyApiUrl + 'TableAreas');
  }

  // Llamamos al controlados de la API en la ruta TableTikets para obtener los datos necesarios para la tabla de tickets.component
  tableTickets() : Observable<any> {
    return this._http.get( this.MyAppurl + this.MyApiUrl + 'TableTikets' );
  }
}
