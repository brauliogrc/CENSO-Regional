import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private MyApiUrl: string = 'Ticket/';
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  // Ticketid es necesario para realizar la consulta de los datos en el "respuesta-folio.component"
  private ticket: number = 0;
  set setTicket(ticketId: number) {
    this.ticket = ticketId;
  }
  get getTicket(): number {
    return this.ticket;
  }

  constructor(private _http: HttpClient) {}

  // Obtencion de los datos para la respuesta del ticket
  getTicketData(ticketId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'ticketData/' + ticketId,
      { headers: this.headers }
    );
  }

  // Borrado logico de un ticket
  deleteTicket(ticketId: number): Observable<any> {
    return this._http.delete(
      `${environment.API_URL}` + this.MyApiUrl + 'deleteTicket/' + ticketId,
      { headers: this.headers }
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
        ticketId,
      { headers: this.headers }
    );
  }

  // Consulta del status de un ticket anonimo
  getAnonTicketStatus(ticketId: number): Observable<any> {
    return this._http.get(
      `${environment.API_URL}` + this.MyApiUrl + 'anonTicketStatus/' + ticketId
    );
  }
}
