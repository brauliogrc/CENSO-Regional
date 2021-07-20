import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { locationList } from 'src/app/interfaces/newInterfaces';
import { environment } from 'src/environments/environment';

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
}
