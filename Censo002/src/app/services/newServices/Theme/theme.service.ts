import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addThemeRelationship } from '../../../../assets/ts/interfaces/newInterfaces';
import {
  addTheme,
  itemChanges,
} from '../../../../assets/ts/interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private MyApiUrl: string = 'Theme/';
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Registro de un nuevo tema en la tabale Theme
  addNewTheme(newTheme: addTheme): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newTheme',
      newTheme,
      { headers: this.headers }
    );
  }

  // Borrado logico de un tema
  deleteTheme(themeId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteTheme/' + themeId,
      { headers: this.headers }
    );
  }

  // Actualizacion del tema
  themeUpdate(newThemeData: itemChanges): Observable<any> {
    console.log( 'Service: ', newThemeData);
    

    return this._http.patch(
      `${environment.API_URL}` + this.MyApiUrl + 'themeUpdate',
      newThemeData,
      { headers: this.headers }
    );
  }

  // Eliminación de relacion entere un tema y una localidad
  deleteRelatedLocation(locationId: number, themeId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'deleteRelatedLocation/' +
        locationId +
        '/' +
        themeId,
      { headers: this.headers }
    );
  }

  // Añadir una relacion entre el tema y la localidad
  addRelatedLocation(relationship: addThemeRelationship): Observable<any> {
    console.log(relationship);

    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'addRelatedLocation',
      relationship,
      { headers: this.headers }
    );
  }
}
