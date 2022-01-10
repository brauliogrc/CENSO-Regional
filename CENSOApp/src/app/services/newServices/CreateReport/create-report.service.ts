import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';

const EXCEL_TYPE: string =
  'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXT: string = '.xlsx';

@Injectable({
  providedIn: 'root',
})
export class CreateReportService {
  private MyApiUrl: string = 'Report/ticketsReport/';
  private headers = new HttpHeaders({
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
  });

  constructor(private _http: HttpClient) {}

  // Obtención de la información de los ticket para e reporte
  getReportData(locationId: number, employeeNumber: number): Observable<any> {
    return this._http
      .get(`${environment.API_URL}` + this.MyApiUrl + locationId + '/' + employeeNumber, {
        headers: this.headers,
      })
      .pipe(
        catchError((err: any) => {
          console.warn(
            'Error en la obtencion de los ticket para la genración del reporte'
          );
          return throwError(
            'Error en la obtencion de los ticket para la genración del reporte'
          );
        })
      );
  }

  // Exportacion de los datos en un xlsx
  exportToExcel(json: any[], excelFileName: string): void {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
    const workbook: XLSX.WorkBook = {
      Sheets: { data: worksheet },
      SheetNames: ['data'],
    };
    const excelBuffer: any = XLSX.write(workbook, {
      bookType: 'xlsx',
      type: 'array',
    });
    this.saveAsExcelFile(excelBuffer, excelFileName);
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], { type: EXCEL_TYPE });
    FileSaver.saveAs(
      data,
      fileName + '_export_' + new Date().getTime() + EXCEL_EXT
    );
  }
}
