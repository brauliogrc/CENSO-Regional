import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreateReportService } from '../services/newServices/CreateReport/create-report.service';
import { ConverToObjectArray } from 'src/assets/ts/exportDataFormat';
import { ExportData } from '../../assets/ts/interfaces/exportData';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css'],
})
export class ReportesComponent implements OnInit {


  constructor(
    private _createReport: CreateReportService,
    private router: Router
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
      sessionStorage.clear();
      this.router.navigate(['/login']);
      console.clear();
      return;
    }
    this.getReportData();
  }

  private objectArray = new ConverToObjectArray();

  ticketList: ExportData[];

  getReportData(): void {
    this._createReport
      .getReportData(Number(sessionStorage.getItem('location')))
      .subscribe((data: any) => {
        this.tickets(data);
        this.anonTickets(data);
        this.getList();
      });
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
    console.log(this.objectArray.ticketList);
  }

  exportToExcel() {
    this._createReport.exportToExcel(this.ticketList, 'TicketsCENSO');
  }
}
