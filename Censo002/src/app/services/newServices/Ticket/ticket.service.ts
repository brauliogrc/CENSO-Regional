import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private MyApiUrl: string = 'Ticket/';

  tiket: number = 0;

  constructor(private _http: HttpClient) {}

  // Obtencion de los datos para la respuesta del ticket
  getTicketData(ticketId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'ticketData/' + ticketId
    );
  }

  // Borrado logico de un ticket
  deleteTicket(ticketId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteTicket/' + ticketId
    );
  }

  // Consulta del status de un ticket
  getTicketStatus(employeeNumber: number, ticketId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` +
        this.MyApiUrl +
        'ticketStatus/' +
        employeeNumber +
        '/' +
        ticketId
    );
  }

  // Consulta del status de un ticket anonimo
  getAnonTicketStatus(ticketId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'anonTicketStatus/' + ticketId
    );
  }
}
