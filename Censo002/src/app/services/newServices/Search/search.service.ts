import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { locationList, questionList } from 'src/app/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';
import { searchData, userList, themeList, ticketList } from '../../../interfaces/newInterfaces';

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
  searchTicket(searchData: searchData): Observable<ticketList> {
    return this._http.get<ticketList>(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'ticketSearch/' +
        searchData.locationId +
        '/' +
        searchData.itemId
    );
  }
}
