import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreateReportService } from '../services/newServices/CreateReport/create-report.service';
import { ConverToObjectArray } from 'src/assets/ts/exportDataFormat';
import { ExportData } from '../../assets/ts/interfaces/exportData';
import { ShowErrorService } from '../services/newServices/ShowErrors/show-error.service';
import { HttpErrorResponse } from '@angular/common/http';
import {
  Theme,
  ticketList,
} from '../../../productivo/assets/ts/interfaces/newInterfaces';
import { FieldsService } from '../services/newServices/Fields/fields.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css'],
})
export class ReportesComponent implements OnInit {
  Theme: Theme[] = [];
  private locationId: number = Number(sessionStorage.getItem('location'));

  constructor(
    private router: Router,
    private _fb: FormBuilder,
    private _fields: FieldsService,
    private _showError: ShowErrorService,
    private _createReport: CreateReportService
  ) {}

  ngOnInit(): void {
    console.log('Iniciadno');
    this.validRole();
  }

  validRole(): void {
    if (
      Number(sessionStorage.getItem('role')) != 1 &&
      Number(sessionStorage.getItem('role')) != 2
    ) {
      console.error('Sección no accesible');
      this._showError.NotAccessible();
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getReportData();
    this.getTheme();
  }

  getTheme(): void {
    this.Theme = [];
    this._fields.getThme(this.locationId).subscribe(
      (data) => {
        this.Theme = [...data];
      },
      (error: HttpErrorResponse) => {
        console.error(error.error.message);
        this._showError.statusCode(error);
      }
    );
  }

  private objectArray = new ConverToObjectArray();

  ticketList: ExportData[];
  private back: ExportData[];

  getReportData(): void {
    this._createReport
      .getReportData(Number(sessionStorage.getItem('location')))
      .subscribe(
        (data: any) => {
          this.tickets(data);
          this.anonTickets(data);
          this.getList();
        },
        (error: HttpErrorResponse) => {
          this._showError.statusCode(error);
        }
      );
  }

  tickets(data: any) {
    for (let ticket of data.tickets) {
      this.objectArray.addTciket(ticket);
    }
  }
  anonTickets(data: any) {
    for (let anonTicket of data.anonTickets) {
      this.objectArray.addAnonTicket(anonTicket);
    }
  }

  getList() {
    this.ticketList = this.objectArray.ticketList;
    this.back = this.objectArray.ticketList;
    console.log(this.objectArray.ticketList);
  }

  exportToExcel() {
    this._createReport.exportToExcel(this.ticketList, 'TicketsCENSO');
  }

  // Filtrado de los tickets
  filterBy = this._fb.group({
    from: [''],
    to: [''],
    theme: [''],
  });

  filter(): void {
    this.ticketList = [...this.back];

    console.log(this.filterBy.value.theme);
    

    if (this.filterBy.value.from != '') {
      this.ticketList = this.ticketList.filter(
        (newList) => newList.creationDate >= new Date(this.filterBy.value.from)
      );
    }
    if (this.filterBy.value.to != '') {
      this.ticketList = this.ticketList.filter(
        (newList) => newList.creationDate <= new Date(this.filterBy.value.to)
      );
    }
    if (this.filterBy.value.theme != '') {
      this.ticketList = this.ticketList.filter(
        (newList) => newList.theme === this.filterBy.value.theme
      );
    }

    console.log(this.ticketList);
  }

  today(): string {
    let day: number | string = new Date().getDate();
    let month: number | string = new Date().getMonth() + 1;
    const year = new Date().getFullYear();

    day < 10 ? (day = '0' + day) : day;
    month < 10 ? (month = '0' + month) : month;

    const fullDate: string = year + '-' + month + '-' + day;
    console.log(fullDate);

    return fullDate;
  }
}
