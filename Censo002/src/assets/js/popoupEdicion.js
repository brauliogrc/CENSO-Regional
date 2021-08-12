const mostrarContenidoModalpopup = document.getElementById('mostrarContenidoModalpopup');
const ModalEditBack = document.getElementById('ModalEditBack');
const quitarContenidoModalpopup = document.getElementById('quitarContenidoModalpopup');

mostrarContenidoModalpopup.addEventListener('click', () => {
    ModalEditBack.classList.add('active');
    //ModalEditBack.getElementById('ModalEditBack').classList.add('active');
    document.getElementById('ModalEdit').style.pointerEvents = 'all';
    document.getElementById('ModalEdit').style.opacity = 1;
});

quitarContenidoModalpopup.addEventListener('click', () => {
    ModalEditBack.classList.remove('active');
    document.getElementById('ModalEdit').style.pointerEvents = 'none';
    document.getElementById('ModalEdit').style.opacity = 0;
});

const mostrarContenidoModalpopupTemas = document.getElementById('mostrarContenidoModalpopupTemas');
const ModalEditBackTemas = document.getElementById('ModalEditBackTemas');
const quitarContenidoModalpopupTemas = document.getElementById('quitarContenidoModalpopupTemas');

mostrarContenidoModalpopupTemas.addEventListener('click', () => {
    ModalEditBackTemas.classList.add('active');
    document.getElementById('ModalEditTemas').style.pointerEvents = 'all';
    document.getElementById('ModalEditTemas').style.opacity = 1;
});

quitarContenidoModalpopupTemas.addEventListener('click', () => {
    ModalEditBackTemas.classList.remove('active');
    document.getElementById('ModalEditTemas').style.pointerEvents = 'none';
    document.getElementById('ModalEditTemas').style.opacity = 0;
});
