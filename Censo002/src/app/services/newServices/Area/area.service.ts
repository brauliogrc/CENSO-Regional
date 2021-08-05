import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { addArea } from '../../../interfaces/newInterfaces';

@Injectable({
  providedIn: 'root',
})
export class AreaService {
  private MyApiUrl: string = 'Area/';

  constructor(private _http: HttpClient) {}

  // Registro de una nueva area en la tabla Areas
  addNewArea(newArea: addArea): Observable<any> {
    return this._http.post(
      `${environment.API_URL}` + this.MyApiUrl + 'newArea/',
      newArea
    );
  }

  // Borrado lógico de un area
  deleteArea(areaId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteArea/' + areaId
    );
  }
}
