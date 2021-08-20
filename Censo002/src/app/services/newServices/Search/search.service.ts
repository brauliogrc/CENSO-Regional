import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {
  existingUser,
  locationList,
  questionList,
} from 'src/assets/ts/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';
import { Theme, existingLocation, existingTheme } from '../../../../assets/ts/interfaces/newInterfaces';
import {
  areaList,
  userTickets,
} from '../../../../assets/ts/interfaces/newInterfaces';
import {
  searchData,
  userList,
  themeList,
  ticketList,
} from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  private MyApiUrl: string = 'Search/';

  constructor(private _http: HttpClient) {}

  // Busqueda de una localidad especifica
  searchLocation(locationId: number): Observable<locationList> {
    return this._http.get<locationList>(
      `${environment.API_URL}` + this.MyApiUrl + 'locationSearch/' + locationId
    );
  }

  // Busqueda de un usuario especifico en una localidad
  searchUser(searchData: searchData): Observable<userList> {
    return this._http.get<userList>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'userSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId
    );
  }

  // Busqueda de un tema especifico en una localidad
  searchTheme(searchData: searchData): Observable<themeList> {
    return this._http.get<themeList>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'themeSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId
    );
  }

  // Busqieda de una pregunta especifica en una localidad
  searchQuestion(searchData: searchData): Observable<questionList> {
    return this._http.get<questionList>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'questionSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId
    );
  }

  // Busqueda de un ticket en una localidad
  searchTicket(searchData: searchData): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'ticketSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId
    );
  }

  // Busqueda de un area en una localidad
  searchArea(searchData: searchData): Observable<areaList> {
    return this._http.get<areaList>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'areaSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId
    );
  }

  // Busqueda de los tickets relacionados al usuario logueado
  getUserTickets(employeeNumber: number): Observable<userTickets[]> {
    return this._http.get<userTickets[]>(
      `${environment.API_URL}` + this.MyApiUrl + 'userTickets/' + employeeNumber
    );
  }

  // OBTENCION DE DATOS PARA LA ACTIAIZACIÓN DE CAMPOS

  // Obtencion de los datos del usuario seleccionado
  getExistingUser(employeeNumber: number): Observable<existingUser[]> {
    return this._http.get<existingUser[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'existingUser/' +
        employeeNumber
    );
  }

  // Obtencion de los temas relacionados al usuario a actualizar
  getRelatedTopics(employeeNumber: number): Observable<Theme[]> {
    return this._http.get<Theme[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'relatedTopics/' +
        employeeNumber
    );
  }

  // Obtención de los datos de la localidad seleccionado
  getExistingLocation(locationId: number): Observable<existingLocation[]> {
    return this._http.get<existingLocation[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'existingLocation/' +
        locationId
    );
  }

  // Obtencion de los datos del tema seleccionado
  getExistingTheme(themeId: number): Observable<existingTheme[]> {
    return this._http.get<existingTheme[]>(
      `${environment.API_URL}` + this.MyApiUrl + 'existingTheme/' + themeId
    );
  }
}
