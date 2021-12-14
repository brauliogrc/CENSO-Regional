import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  existingUser,
  locationList,
  questionList,
  Theme,
  existingLocation,
  existingTheme,
  Location,
  areaList,
  userTickets,
  searchData,
  userList,
  themeList,
  existingQuestion,
  searchData2,
} from 'src/assets/ts/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  private MyApiUrl: string = 'Search/';
  private headers = new HttpHeaders({
    Authorization: `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Busqueda de una localidad especifica
  searchLocation(locationId: string): Observable<locationList[]> {
    return this._http.get<locationList[]>(
      `${environment.API_URL}` + this.MyApiUrl + 'locationSearch/' + locationId,
      { headers: this.headers }
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
        searchData.itemId,
      { headers: this.headers }
    );
  }

  // Busqueda de un tema especifico en una localidad
  searchTheme(searchData: searchData): Observable<themeList[]> {
    return this._http.get<themeList[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'themeSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId,
      { headers: this.headers }
    );
  }

  // Busqieda de una pregunta especifica en una localidad
  searchQuestion(searchData: searchData): Observable<questionList[]> {
    return this._http.get<questionList[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'questionSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId,
      { headers: this.headers }
    );
  }

  // Busqueda de un ticket en una localidad
  searchTicket(searchData: searchData2): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'ticketSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId,
      { headers: this.headers }
    );
  }

  // Busqueda de un area en una localidad
  searchArea(searchData: searchData): Observable<areaList[]> {
    return this._http.get<areaList[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'areaSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId,
      { headers: this.headers }
    );
  }

  // Busqueda de los tickets relacionados al usuario logueado
  getUserTickets(employeeNumber: number): Observable<userTickets[]> {
    return this._http.get<userTickets[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'userTickets/' +
        employeeNumber,
      { headers: this.headers }
    );
  }

  // OBTENCION DE DATOS PARA LA ACTIAIZACIÓN DE CAMPOS

  // Obtencion de los datos del usuario seleccionado
  getExistingUser(employeeNumber: number): Observable<existingUser[]> {
    return this._http.get<existingUser[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'existingUser/' +
        employeeNumber,
      { headers: this.headers }
    );
  }

  // Obtencion de los temas relacionados al usuario a actualizar
  getRelatedTopics(employeeNumber: number): Observable<Theme[]> {
    return this._http.get<Theme[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'relatedUserTopics/' +
        employeeNumber,
      { headers: this.headers }
    );
  }

  // Obtención de los temas relacionados a la pregunta a actualizar
  getRelatedTopicsQ(questionId: number): Observable<Theme[]> {
    return this._http.get<Theme[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'relatedQuestionTopics/' +
        questionId,
      { headers: this.headers }
    );
  }

  // Obtencion de las localidades relacionadas con el tema a actualizar
  getRelatedLocations(themeId: number): Observable<Location[]> {
    return this._http.get<Location[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'relatedThemeLocations/' +
        themeId,
      { headers: this.headers }
    );
  }

  // Obtención de los datos de la localidad seleccionado
  getExistingLocation(locationId: number): Observable<existingLocation[]> {
    return this._http.get<existingLocation[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'existingLocation/' +
        locationId,
      { headers: this.headers }
    );
  }

  // Obtencion de los datos del tema seleccionado
  getExistingTheme(themeId: number): Observable<existingTheme[]> {
    return this._http.get<existingTheme[]>(
      `${environment.API_URL}` + this.MyApiUrl + 'existingTheme/' + themeId,
      { headers: this.headers }
    );
  }

  // Obención de los datos de la pregunta seleccionada
  getExistingQuestion(questionId: number): Observable<existingQuestion[]> {
    return this._http.get<existingQuestion[]>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'existingQuestion/' +
        questionId,
      { headers: this.headers }
    );
  }

  // Obtencion de los datos del area seleccionada
  getExistingArea(areaId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'existingArea/' + areaId,
      { headers: this.headers }
    );
  }
}
