import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import {
  ticketList,
  anonTicketList,
  searchData,
} from '../interfaces/newInterfaces';
import { ListService } from '../services/newServices/List/list.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { TicketService } from '../services/newServices/Ticket/ticket.service';

@Component({
  selector: 'app-tikets',
  templateUrl: './tikets.component.html',
  styleUrls: ['./tikets.component.css'],
})
export class TiketsComponent implements OnInit {
  Tikets: ticketList[] = [];
  AnonTikets: anonTicketList[] = [];

  tiket: any;
  anonTicket: any;

  flag: boolean = false;

  constructor(
    private _listService: ListService,
    private _searchService: SearchService,
    private _ticketService: TicketService,
    private _auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2 &&
      Number(sessionStorage.getItem('role')) != 3
    ) {
      console.error('Sección no accesible');
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.flag = false;
    this.getTiketsList();
  }

  // Obtencion de listado de tikets (tanto anonimos como con datos)
  getTiketsList(): void {
    this._listService
      .getTicketList(Number(sessionStorage.getItem('location')))
      .subscribe(
        (data) => {
          this.Tikets = [...data.tickets];
          this.AnonTikets = [...data.anonTickets];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
  }

  // Borrado logico de un ticket
  deleteTicket(ticketId: number): void {
    this._ticketService.deleteTicket(ticketId).subscribe(
      (data) => {
        console.log(data.message);
        this.getTiketsList();
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Busqueda de un tiket mediante su id
  search(ticketId: string) {
    if (ticketId) {
      let ticketSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: Number(ticketId),
      };

      this._searchService.searchTicket(ticketSearch).subscribe(
        (data) => {
          this.flag = true;
          if (data.rUserName[0] != null) {
            this.tiket = data;
            this.anonTicket = null;
            console.log(this.tiket[0]);
          } else {
            this.anonTicket = data;
            this.tiket = null;
            console.log(this.anonTicket[0]);
          }
          this.Tikets = [];
          this.AnonTikets = [];
        },
        (error) => {
          console.error(error.error.message);
        }
      );
    }
  }

  // Obtencion de los datos de la tabla al hacer click en una row
  ticketResponse(ticketId: number) {
    this._ticketService.setTicket = ticketId;
    this.router.navigate(['/respuestafolio']);
  }
}
