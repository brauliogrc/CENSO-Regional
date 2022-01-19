import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/Auth/auth.service';
import { Router } from '@angular/router';
import {
  ticketList,
  anonTicketList,
  searchData,
  // searchData2,
} from '../../assets/ts/interfaces/newInterfaces';
import { ListService } from '../services/newServices/List/list.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { TicketService } from '../services/newServices/Ticket/ticket.service';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-tikets',
  templateUrl: './tikets.component.html',
  styleUrls: ['./tikets.component.css'],
})
export class TiketsComponent implements OnInit {
  Tikets: ticketList[] = [];
  private respaldoTickets: ticketList[] = [];
  AnonTikets: anonTicketList[] = [];
  private respaldoAnonTickets: anonTicketList[] = [];

  tiket: any;
  anonTicket: any;

  flag: boolean = false;

  constructor(
    private router: Router,
    private _auth: AuthService,
    private _listService: ListService,
    private _showError: ShowErrorService,
    private _searchService: SearchService,
    private _ticketService: TicketService,
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
      this._showError.NotAccessible();
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
      .getTicketList( Number(sessionStorage.getItem('location')), Number(sessionStorage.getItem('employeeNumber')) )
      .subscribe(
        (data) => {
          this.Tikets = [...data.tickets];
          this.AnonTikets = [...data.anonTickets];
          this.respaldoTickets = [...data.tickets];
          this.respaldoAnonTickets = [...data.anonTickets];
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
  }

  // Borrado logico de un ticket
  deleteTicket(ticketId: number): void {
    this._ticketService.deleteTicket(ticketId).subscribe(
      (data) => {
        // console.log(data.message);
        this._showError.success(data.message);
        this.getTiketsList();
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  // Busqueda de un tiket mediante su id
  search(ticketId: string) {
    if (ticketId) {
      let ticketSearch: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: String( ticketId ),
      };

      this._searchService.searchTicket(ticketSearch).subscribe(
        (data) => {
          // console.log(data);
          this.Tikets = [];
          this.AnonTikets = [];

          this.flag = true;
          if ( data.ticket != null ) {
            this.Tikets = [... data.ticket];
            // console.log(this.Tikets);
          }
          if ( data.anonTicket != null ) {
            this.AnonTikets = [...data.anonTicket];
            // console.log(this.AnonTikets);
          }
          // if (data[0].rUserName != null) {
          //   this.tiket = data;
          //   this.anonTicket = null;
          //   console.log(this.tiket[0]);
          // } else {
          //   this.anonTicket = data;
          //   this.tiket = null;
          //   console.log(this.anonTicket[0]);
          // }
          // this.Tikets = [];
          // this.AnonTikets = [];
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
    }
  }

  public clearFilter(): void {
    this.Tikets = [...this.respaldoTickets];
    this.AnonTikets = [...this.respaldoAnonTickets];
  }

  // Obtencion de los datos de la tabla al hacer click en una row
  ticketResponse(ticketId: number) {
    this._ticketService.setTicket = ticketId;
    this.router.navigate(['/respuestafolio']);
  }

  // Navegar al panel usuario
  addNewTicket() {
    this.router.navigate(['/panelusuario']);
  }
}
