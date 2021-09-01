// Solo hace que aparaesca la ventana del modal
function mostrar() {
    // ModalEditBack.getElementById('ModalEditBack').classList.add('active');
    document.getElementById('ModalEditBack').classList.add('active');
    document.getElementById('ModalEdit').style.pointerEvents = 'all';
    document.getElementById('ModalEdit').style.opacity = 1;
}
function cerrar() {
    // ModalEditBack.getElementById('ModalEditBack').classList.add('active');
    document.getElementById('ModalEditBack').classList.remove('active');
    document.getElementById('ModalEdit').style.pointerEvents = 'none';
    document.getElementById('ModalEdit').style.opacity = 0;
}


// Modal de añadir temas
function mostrarTema() {
    document.getElementById('ModalEditBackTemas').classList.add('active');
    document.getElementById('ModalEditTemas').style.pointerEvents = 'all';
    document.getElementById('ModalEditTemas').style.opacity = 1;
}
function cerrarTema() {
    document.getElementById('ModalEditBackTemas').classList.remove('active');
    document.getElementById('ModalEditTemas').style.pointerEvents = 'none';
    document.getElementById('ModalEditTemas').style.opacity = 0;
}