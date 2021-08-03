import { Component, Input, OnInit, Pipe } from '@angular/core';
import {
  DomSanitizer,
  SafeResourceUrl,
  SafeUrl,
  SafeValue,
} from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { TicketService } from '../services/newServices/Ticket/ticket.service';
import { LocationValidate } from '../services/validations';

@Component({
  selector: 'app-respuesta-folio',
  templateUrl: './respuesta-folio.component.html',
  styleUrls: ['./respuesta-folio.component.css'],
})
export class RespuestaFolioComponent implements OnInit {
  ticketData: any;
  anonTicketData: any;
  daysPassed: number = 0;

  flag: boolean = false;
  private ticketId: number = 0;

  constructor(
    private _ticketService: TicketService,
    private router: Router,
    private _auth: AuthService,
    private _sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.validRole();
  }

  // Validacion del rol del usuario
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
    this.ticketId = this._ticketService.tiket;
    console.log(this.ticketId);
    this.getTicketData();
  }

  evidenceRoute: SafeValue = '';
  value: any;
  // Obtencion de los datos del ticket
  getTicketData(): void {
    this._ticketService.getTicketData(this.ticketId).subscribe(
      (data) => {
        if (data.anonTicketData) {
          this.anonTicketData = data.anonTicketData[0];

          data.anonTicketData[0].arEmployeeType = this.getEmployeeType(
            data.anonTicketData[0].arEmployeeType
          );

          console.log(this.anonTicketData);

          // Sanitizacion de la URL de la evidencia
          // this.evidenceRoute = this._sanitizer.bypassSecurityTrustUrl(
          //   `url(${this.anonTicketData.arAttachement})`
          // );
          // console.log({ this: this.evidenceRoute });

          // this.value = window.location.href;
          // console.log(this.value);
          // this.value = '';
          // console.log( this.value );
          // this.value = this.anonTicketData.arAttachement;
          // console.log(this.value);

          this.flag = true;
        } else {
          this.ticketData = data.ticketData[0];

          data.ticketData[0].rEmployeeType = this.getEmployeeType(
            data.ticketData[0].rEmployeeType
          );

          console.log(this.ticketData);
          this.flag = true;
        }
        this.daysPassed = data.daysPassed;
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtencion del tipo de empleado
  getEmployeeType(employeeType: number): string | null {
    const locationValidate = new LocationValidate();
    return locationValidate.employeeTypeValidation(employeeType);
  }
}
