import Swal from 'sweetalert2';

const NotFound = (error: any) => {
  Swal.fire({
    title: 'Not Found',
    text: error,
    icon: 'question',
    backdrop: false,
    position: 'top',
  });
};

const BadRequest = (error: any) => {
  Swal.fire({
    title: 'Bad Request',
    text: `Por favor pongase en contacto con soporte. Error: ${error}`,
    icon: 'question',
    backdrop: false,
    position: 'top',
  });
};

export const statusCode = (code: number, error: any) => {
  if (code === 404) NotFound(error);
  if (code === 400) BadRequest(error);
};
