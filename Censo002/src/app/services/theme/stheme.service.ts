import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SthemeService {
  private MyAppUrl: string = 'https://localhost:44358/';
  private MyApiUrl: string = 'api/Theme';

  constructor(private _http: HttpClient) {}

  addNewTheme(dataTheme: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.post(this.MyAppUrl + this.MyApiUrl, dataTheme, {
      headers: headers,
    });
  }

  // cambiar ruta a EmpyCensoController
  getSpecificsThems(locationId: any): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.get(
      this.MyAppUrl + this.MyApiUrl + '/availableThemes/' + locationId,
      { headers: headers }
    );
  }

  deleteTheme(id: number): Observable<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${sessionStorage.getItem('token')}`,
    });
    return this._http.delete(this.MyAppUrl + this.MyApiUrl + '/' + id, {
      headers: headers,
    });
  }
}
