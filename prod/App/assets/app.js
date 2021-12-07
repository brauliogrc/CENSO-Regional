const open = document.getElementById('open');
const ModalContenido = document.getElementById('ModalContenido');
const close = document.getElementById('close');

open.addEventListener('click', () => {
    ModalContenido.classList.add('show');
});

close.addEventListener('click', () => {
    ModalContenido.classList.remove('show');
});


const open = document.getElementById('open');
const ModalContenido = document.getElementById('ModalContenido');
const close = document.getElementById('close');

open.addEventListener('click', () => {
    ModalContenido.classList.add('show');
    document.getElementById('modalagregar').style.display = 'none';
    document.getElementById('modalagregar1').style.display = 'none';

});

close.addEventListener('click', () => {
    ModalContenido.classList.remove('show');
    document.getElementById('modalagregar').style.display = 'block';
    document.getElementById('modalagregar1').style.display = 'block';
});

