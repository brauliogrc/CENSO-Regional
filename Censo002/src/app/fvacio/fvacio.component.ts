import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/Auth/auth.service';
import { TicketService } from '../services/newServices/Ticket/ticket.service';

@Component({
  selector: 'app-fvacio',
  templateUrl: './fvacio.component.html',
  styleUrls: ['./fvacio.component.css'],
})
export class FvacioComponent implements OnInit {
  ticketStatus: any;
  responsable: any;
  flag: boolean = false;

  constructor(
    private _authService: AuthService,
    private _ticketService: TicketService
  ) {}

  ngOnInit(): void {
    this.flag = false;
  }

  // Consulta el estado del folio
  getTicketStatus(ticketId: string): void {
    if (ticketId) {
      this._ticketService
        .getTicketStatus(
          Number(sessionStorage.getItem('employeeNumber')),
          Number(ticketId)
        )
        .subscribe(
          (data) => {
            this.ticketStatus = data.ticket[0];
            console.log(this.ticketStatus);
            if (data.answer) {
              this.responsable = data.answer;
            } else {
              console.log(data.message);
            }
            this.flag = true;
          },
          (error) => {
            console.error(error.error.message);
          }
        );
    }
  }

  // Llamada al metodo de cerrar sesion
  logout() {
    this._authService.logout();
  }
}
