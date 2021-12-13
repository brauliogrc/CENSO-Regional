import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { TicketService } from '../services/newServices/Ticket/ticket.service';
import { SearchService } from '../services/newServices/Search/search.service';
import { userTickets } from '../../assets/ts/interfaces/newInterfaces';
import { App2 } from '../../assets/ts/app2';
import { HttpErrorResponse } from '@angular/common/http';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';

@Component({
  selector: 'app-fvacio',
  templateUrl: './fvacio.component.html',
  styleUrls: ['./fvacio.component.css'],
})
export class FvacioComponent implements OnInit {
  public ticketStatus: any;
  public responsable: any;
  public searchFlag: boolean = false;
  public haveAnswer: boolean = false;
  public flag: boolean = false;
  public userTickets: userTickets[] = [];

  public haveAttachement: boolean = false;

  app = new App2();

  constructor(
    private router: Router,
    private _search: SearchService,
    private _authService: AuthService,
    private _showError: ShowErrorService,
    private _ticketService: TicketService
  ) {}

  ngOnInit(): void {
    this.searchFlag = false;
    this.haveAnswer = false;
    this.flag = false;
    this.getUserTickets();
  }

  // Consulta el estado del folio
  getTicketStatus(ticketId: any): void {
    if (ticketId) {
      this._ticketService
        .getTicketStatus(
          Number(sessionStorage.getItem('employeeNumber')),
          Number(ticketId)
        )
        .subscribe(
          (data) => {
            this.app.mostrarbusq();
            this.searchFlag = true;
            this.ticketStatus = data.ticket[0];
            // console.log(this.ticketStatus);

            if (data.answer) {
              this.haveAnswer = true;
              this.responsable = data.answer[0];
              if ( data.answer[0].asAttachement != null ) {
                this.haveAttachement = true;
              }
              // console.log(this.responsable);
              
            } else {
              // console.log(data.message);
              this._showError.success(data.message);
            }

            this.flag = true;
          },
          (error: HttpErrorResponse) => {
            console.error(error.error.message);
            this._showError.statusCode(error);
          }
        );
    }
  }

  // Obtncón de los tickets relacionados con el usurio
  getUserTickets(): void {
    this._search
      .getUserTickets(Number(sessionStorage.getItem('employeeNumber')))
      .subscribe(
        (data) => {
          this.userTickets = [...data];
          // console.log(data);
        },
        (error: HttpErrorResponse) => {
          console.error(error.error.message);
          this._showError.statusCode(error);
        }
      );
  }

  // Llamada al metodo de cerrar sesion
  logout(): void {
    this._authService.logout();
  }

  // Navegar al panel usuario
  addNewTicket(): void {
    this.router.navigate(['/panelusuario']);
  }

  // Cerrar popup que muestra el contenido de la infoamción del ticket
  cerrarPopup(): void {
    this.app.cerrarbusq();
    this.searchFlag = false;
    this.haveAnswer = false;
  }
}
