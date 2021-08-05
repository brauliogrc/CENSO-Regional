import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addTheme } from '../../../interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private MyApiUrl: string = 'Theme/';

  constructor(private _http: HttpClient) {}

  // Registro de un nuevo tema en la tabale Theme
  addNewTheme(newTheme: addTheme): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newTheme',
      newTheme
    );
  }

  // Borrado logico de un tema
  deleteTheme(themeId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteTheme/' + themeId
    );
  }
}
