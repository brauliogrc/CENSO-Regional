// const { Swal } = require("./SweetAlert-min");

const NotFound = (error) => {
    Swal.fire({
        text: error,
        icon: 'question',
        backdrop: false,
        position: 'top'
    });
}
module.exports = {
    NotFound
};

function showAlert1() {
    Swal.fire({

        text: 'Ninguna localidad encontrada en la base de datos',

        icon: 'question',

        backdrop: false,

        position: 'top'

    });

}

function showAlert2() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la lista de localidades.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert3() {
    Swal.fire({
        text: 'Ningun usuario se encuntra asociado a su localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert4() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la lista de usuarios.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert5() {
    Swal.fire({
        text: 'Ningun tema se encuentra asociado con tu localidad',
        icon: 'question',
        backdrop: false,
        position: 'top'

    });
}
function showAlert6() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la lista de temas.',
        icon: 'error',
        backdrop: false,
        position: 'top'

    });
}
function showAlert7() {
    Swal.fire({
        text: 'Ninguna pregunta se encuentra asociada con tu localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert8() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la lista de preguntas.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert9() {
    Swal.fire({
        text: 'Ningun ticket se encuentra asociado con tu localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert10() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la lista de tickets.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert11() {
    Swal.fire({
        text: 'Ninguna area se encuentra asociada a tu localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert12() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la lista de areas',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert13() {
    Swal.fire({
        text: 'La localidad no se encuentra en la base de datos',
        icon: 'question',
        backdrop: false,
        position: 'top'
    });
}
function showAlert14() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la localidad.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert15() {
    Swal.fire({
        text: 'El usuario no se encuentra en la localidad',
        icon: 'question',
        backdrop: false,
        position: 'top'
    });
}
function showAlert16() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener el usuario.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert17() {
    Swal.fire({
        text: 'El tema no se encuentra en la localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert18() {
    Swal.fire({
        text: 'Ha ocuttido un error al obtener el tema.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert19() {
    Swal.fire({
        text: 'La pregunta no se encuentra en la localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert20() {
    Swal.fire({
        text: 'Ha ocuttido un error al obtener la Pregunta.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert21() {
    Swal.fire({
        text: 'El ticket no se encuentra en la localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert22() {
    Swal.fire({
        text: 'Ha ocuttido un error al obtener el ticket.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert23() {
    Swal.fire({
        text: 'Ninguna localidad encontrada en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert24() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener las localidades.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert25() {
    Swal.fire({
        text: 'Ningun tema se encuentra relacionado con esta localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert26() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener los temas.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert27() {
    Swal.fire({
        text: 'Ninguna pregunta se encuentra relacionada con este tema',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert28() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener las preguntas.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert29() {
    Swal.fire({
        text: 'Ninguna area se encuentra relacionada con est localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert30() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener las areas.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert31() {
    Swal.fire({
        text: 'Ningun rol encontrado',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert32() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener los roles.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert33() {
    Swal.fire({
        text: 'Peticion {addAnonRequest.arId} registrada con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert34() {
    Swal.fire({
        text: 'Ha ocurrido un error al registrar la peticion.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert35() {
    Swal.fire({
        text: 'La peticion {anonRequestId} no se encuentra en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert36() {
    Swal.fire({
        text: 'La peticion {query.arId}, fue eliminada con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert37() {
    Swal.fire({
        text: 'Ha ocurrido un error al eliminar la peticion.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert38() {
    Swal.fire({
        text: 'Respuesta {addAnswer.asId} del tiket {newAnswer.RequestId}, registrada correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert39() {
    Swal.fire({
        text: 'Respuesta {addAnswer.asId} del tiket {newAnswer.RequestId}, registrada correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert40() {
    Swal.fire({
        text: 'Ha ocurrido un error al registrar la respuesta.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert41() {
    Swal.fire({
        text: 'El tiket no se encuentra en la localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert42() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la información del ticket.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert43() {
    Swal.fire({
        text: 'El usuario ya se encuentra registrado',
        icon: 'info',
        backdrop: false,
        position: 'top'
    });
}
function showAlert44() {
    Swal.fire({
        text: 'Usuario {addUser.uName} con numero de empleado {addUser.uEmployeeNumber}, registrado correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert45() {
    Swal.fire({
        text: 'Ha ocurrido un error al registrar el usuario.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert46() {
    Swal.fire({
        text: 'El usuario ya se enceuntra registrado',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert47() {
    Swal.fire({
        text: 'Usuario no ecnontrado en su localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert48() {
    Swal.fire({
        text: 'Ha ocurrido un error al obtener la informacion del usuario.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert49() {
    Swal.fire({
        text: 'El usuario {userId}, no se encuentra en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert50() {
    Swal.fire({
        text: 'El usuario {userId}, ha sido eliminado con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert51() {
    Swal.fire({
        text: 'Ha ocurido un error al eliminar el usuatio.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert52() {
    Swal.fire({
        text: 'El usuario no cuenta con una localidad',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert53() {
    Swal.fire({
        text: 'La localidad {addLocation.lName}, se ha registrado correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert54() {
    Swal.fire({
        text: 'Ha ocurrido un error al registrar la localidad.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert55() {
    Swal.fire({
        text: 'La localidad {locationId}, no se encuentra en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert56() {
    Swal.fire({
        text: 'La localidad {locationId}, ha sido eliminada correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert57() {
    Swal.fire({
        text: 'Ha ocurido un error al eliminar la localidad.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert58() {
    Swal.fire({
        text: 'La pregunta {addQuestion.qName}, se ha registrado correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert59() {
    Swal.fire({
        text: 'Ha ocurrido un error al registar la pregunta.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert60() {
    Swal.fire({
        text: 'La pregunta {questionId}, no se encuentra en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert61() {
    Swal.fire({
        text: 'La pregunta {questionId}, fue eliminada con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert62() {
    Swal.fire({
        text: 'Ha ocurrido un error al eliminar la pregunta.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert63() {
    Swal.fire({
        text: 'Peticion {addRequest.rId}, registrada con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert64() {
    Swal.fire({
        text: 'Ha ocurrido un error al registrar la peticion.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert65() {
    Swal.fire({
        text: 'La peticion {requestId}, no se encuentra en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert66() {
    Swal.fire({
        text: 'La peticion {requestId}, se ha eliminado con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert67() {
    Swal.fire({
        text: 'Ha ocurrido un error el eliminar la peticion.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert68() {
    Swal.fire({
        text: 'El tema {addTheme.tName}, se ha registrado correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert69() {
    Swal.fire({
        text: 'Ha ocurrido un error al registar el tema.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert70() {
    Swal.fire({
        text: 'El tema {themeId} no se encuentra en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert71() {
    Swal.fire({
        text: 'El tema {themeId}, fue eliminado con exito',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert72() {
    Swal.fire({
        text: 'Ha ocurrido un error al eliminar el tema.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert73() {
    Swal.fire({
        text: 'Ticket no encontrado en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert74() {
    Swal.fire({
        text: 'Ha ocurrido un error en la obtencion de los datos del ticket.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert75() {
    Swal.fire({
        text: 'Ticket eliminado correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert76() {
    Swal.fire({
        text: 'Ticket eliminado correctamente',
        icon: 'success',
        backdrop: false,
        position: 'top'
    });
}
function showAlert77() {
    Swal.fire({
        text: 'Ha ocurrido un error al eliminar el ticket.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}
function showAlert78() {
    Swal.fire({
        text: 'Ticket no encontrado en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert79() {
    Swal.fire({
        text: 'El ticker no ha sido respondido aún',
        icon: 'info',
        backdrop: false,
        position: 'top'
    });
}
function showAlert80() {
    Swal.fire({
        text: 'Ha ocurrido un error al consultar el estado del ticket.',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert81() {
    Swal.fire({
        text: 'Ticket no encontrado en la base de datos',
        icon: 'warning',
        backdrop: false,
        position: 'top'
    });
}
function showAlert82() {
    Swal.fire({
        text: 'El ticker no ha sido respondido aún',
        icon: 'info',
        backdrop: false,
        position: 'top'
    });
}
function showAlert83(error) {
    Swal.fire({
        text: 'Ha ocurrido un error al consultar es estado del ticket.',
        icon: 'error',
        backdrop: false,
        position: 'top'
    });
}


