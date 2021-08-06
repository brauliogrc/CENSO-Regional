import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class ShowErrorService {
  constructor() {}

  statusCode = (error: HttpErrorResponse) => {
    if (Number(error.status) === 0) this.ConnectionReused(String(error.message));
    else if (Number(error.status) === 404) this.NotFound(String(error.error.message));
    else if (Number(error.status) === 400) this.BadRequest(String(error.error.message));
    else this.UnconrolledError(String(error.message));
  };

  private NotFound = (error: string) => {
    Swal.fire({
      // title: '404. Not Found',
      text: error,
      icon: 'question',
      backdrop: false,
      position: 'top',
    });
  };

  private BadRequest = (error: string) => {
    Swal.fire({
      // title: '400. Bad Request',
      text: `Por favor pongase en contacto con soporte. Error: ${error}`,
      icon: 'question',
      backdrop: false,
      position: 'top',
    });
  };

  private ConnectionReused = (error: string) => {
    Swal.fire({
      // title: '0. Error Connection Refused',
      text: `Por favor pongase en contacto con soporte. Error: ${error}`,
      icon: 'question',
      backdrop: false,
      position: 'top',
    });
  };

  private UnconrolledError = (error: string) => {
    Swal.fire({
      title: 'Uncontrolled error',
      text: `Por favor pongase en contacto con soporte. Error: ${error}`,
      icon: 'question',
      backdrop: false,
      position: 'top',
    });
  };
}
