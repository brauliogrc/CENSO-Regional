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
import { AnswerService } from '../services/newServices/Answer/answer.service';
import { searchData, User } from '../interfaces/newInterfaces';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { FormBuilder, Validators } from '@angular/forms';

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

  availableUsers: User[] = [];

  currentUser: any = sessionStorage.getItem('employeeNumber');

  constructor(
    private router: Router,
    private _fb: FormBuilder,
    private _auth: AuthService,
    private _fields: FieldsService,
    private _sanitizer: DomSanitizer,
    private _answerService: AnswerService,
    private _ticketService: TicketService
  ) {}

  // Definición del formulario
  newAnswer = this._fb.group({
    asUserId: ['', [Validators.required]],
    asAnswer: ['', [Validators.required]],
    asAttachement: ['', [Validators.maxLength(200)]],
  });

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
    this.ticketId = this._ticketService.getTicket;
    console.log(this.ticketId);
    this.getTicketData();
    console.log('folio de respuestas');
  }

  // Obtencion de los datos del ticket
  getTicketData(): void {
    this._ticketService.getTicketData(this.ticketId).subscribe(
      (data) => {
        if (data.anonTicketData) {
          this.anonTicketData = data.anonTicketData[0];

          data.anonTicketData[0].arEmployeeType = this.getEmployeeType(
            data.anonTicketData[0].arEmployeeType
          );

          console.log(this.anonTicketData.tId);

          this.flag = true;
          this.getAvailableUsers(this.anonTicketData.tId);
        } else {
          this.ticketData = data.ticketData[0];

          data.ticketData[0].rEmployeeType = this.getEmployeeType(
            data.ticketData[0].rEmployeeType
          );

          console.log(this.ticketData);
          this.flag = true;
          this.getAvailableUsers(this.ticketData.tId);
        }
        this.daysPassed = data.daysPassed;
      },
      (error) => {
        console.error(error.error.message);
      }
    );
  }

  // Obtención de los usuarios asociados al tema del ticket en una localidad
  getAvailableUsers(themeId: number): void {
    if (themeId) {
      const searchData: searchData = {
        locationId: Number(sessionStorage.getItem('location')),
        itemId: themeId,
      };
      this._fields.getAvailableUsers(searchData).subscribe(
        (data) => {
          this.availableUsers = [...data];
          console.log(this.availableUsers);
        },
        (error) => {
          console.error(error.error.message);
        }
      );
    }
  }

  // Guardado de la nueva respuesta
  registerAnswer(): void {
    const formData = new FormData();
    formData.append('asUserId', this.newAnswer.get('asUserId')?.value);
    formData.append('asAnswer', this.newAnswer.get('asAnswer')?.value);
    formData.append('RequestId', String(this.ticketId));
    formData.append('asAttachement', this.file);

    this._answerService.addNewAnswer(formData).subscribe(
      (data) => {
        console.log('Registro exitoso de la respuesta');
        console.log(data.message);
        this.newAnswer.reset();
        this.ticketId = 0;
      },
      (error) => {
        console.log(error.error.message);
      }
    );
  }

  // Manejo del arcvhivo seleccionado
  private file: any;
  onFileSelected = (event: any): void => {
    const file: File = event.target.files[0];

    if (file) {
      this.file = file;
      console.log(file);
    }
  };

  // Obtencion del tipo de empleado
  getEmployeeType(employeeType: number): string | null {
    const locationValidate = new LocationValidate();
    return locationValidate.employeeTypeValidation(employeeType);
  }
}
