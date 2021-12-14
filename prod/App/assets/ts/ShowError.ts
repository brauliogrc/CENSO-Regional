import { HttpErrorResponse } from '@angular/common/http';
import Swal from 'sweetalert2';

export class ShowError {
  statusCode = (error: HttpErrorResponse) => {
    if (error.status === 404) this.NotFound(error.message);
    if (error.status === 400) this.BadRequest(error.message);
  };

  private NotFound = (error: any) => {
    Swal.fire({
      title: 'Not Found',
      text: error,
      icon: 'question',
      backdrop: false,
      position: 'top',
    });
  };

  private BadRequest = (error: any) => {
    Swal.fire({
      title: 'Bad Request',
      text: `Por favor pongase en contacto con soporte. Error: ${error}`,
      icon: 'question',
      backdrop: false,
      position: 'top',
    });
  };
}
