export class App2 {
  constructor() {}

  mostrarbusq(): void {
    const modalContenidoBusq = document.getElementById('ModalContenidoBusq');
    if (modalContenidoBusq) {
      modalContenidoBusq.style.pointerEvents = 'all';
      modalContenidoBusq.style.opacity = '1';
    }
  }

  cerrarbusq(): void {
    const modalContenidoBusq: HTMLElement =
      document.getElementById('ModalContenidoBusq');
    if (modalContenidoBusq) {
      modalContenidoBusq.style.pointerEvents = 'none';
      modalContenidoBusq.style.opacity = '0';
    }
  }
}
