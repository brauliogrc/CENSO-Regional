
export class Popup {
  constructor(){}

  // Muestra el modal (Ventana emergente o popup)
  mostrar() {
    document.getElementById('ModalEditBack')?.classList.add('active');
    const modalEdit = document.getElementById('ModalEdit');
    if (modalEdit) {
      modalEdit.style.pointerEvents = 'all';
      modalEdit.style.opacity = '1';
    }
  };

  cerrar() {
    document.getElementById('ModalEditBack')?.classList.remove('active');
    const modalEdit = document.getElementById('ModalEdit');
    if (modalEdit) {
      modalEdit.style.pointerEvents = 'none';
      modalEdit.style.opacity = '0';
    }
  };

  // Modal de añadir temas
  mostrarTema() {
    document.getElementById('ModalEditBackTemas')?.classList.add('active');
    const modalEditTemas = document.getElementById('ModalEditTemas');
    if (modalEditTemas) {
      modalEditTemas.style.pointerEvents = 'all';
      modalEditTemas.style.opacity = '1';
    }
  };

  cerrarTema() {
    document.getElementById('ModalEditBackTemas')?.classList.remove('active');
    const modalEditTemas = document.getElementById('ModalEditTemas');
    if (modalEditTemas) {
      modalEditTemas.style.pointerEvents = 'none';
      modalEditTemas.style.opacity = '0';
    }
  };
}
