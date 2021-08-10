var mostrarContenidoModalpopup = document.getElementById('mostrarContenidoModalpopup'),
    ModalEditBack = document.getElementById('ModalEditBack'),
    quitarContenidoModalpopup = document.getElementById('quitarContenidoModalpopup');

mostrarContenidoModalpopup.addEventListener('click', function () {
    ModalEditBack.classList.add('active');
    document.getElementById('ModalEdit').style.pointerEvents = 'all';
    document.getElementById('ModalEdit').style.opacity = 1;
});

quitarContenidoModalpopup.addEventListener('click', function () {
    ModalEditBack.classList.remove('active');
    document.getElementById('ModalEdit').style.pointerEvents = 'none';
    document.getElementById('ModalEdit').style.opacity = 0;
});

var mostrarContenidoModalpopupTemas = document.getElementById('mostrarContenidoModalpopupTemas'),
    ModalEditBackTemas = document.getElementById('ModalEditBackTemas'),
    quitarContenidoModalpopupTemas = document.getElementById('quitarContenidoModalpopupTemas');

mostrarContenidoModalpopupTemas.addEventListener('click', function () {
    ModalEditBackTemas.classList.add('active');
    document.getElementById('ModalEditTemas').style.pointerEvents = 'all';
    document.getElementById('ModalEditTemas').style.opacity = 1;
});

quitarContenidoModalpopupTemas.addEventListener('click', function () {
    ModalEditBackTemas.classList.remove('active');
    document.getElementById('ModalEditTemas').style.pointerEvents = 'none';
    document.getElementById('ModalEditTemas').style.opacity = 0;
});
