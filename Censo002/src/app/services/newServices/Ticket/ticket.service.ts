import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private MyApiUrl: string = '';

  constructor(private _http: HttpClient) {}

  // Borrado logico de un ticket
  
}
