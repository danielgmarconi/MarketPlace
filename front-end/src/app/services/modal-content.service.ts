import { Injectable } from '@angular/core';
declare var bootstrap: any;

@Injectable({
  providedIn: 'root'
})
export class ModalContentService {

  constructor() { }
  openModal(id:string): void {
    const modalElement = document.getElementById(id);
    if (modalElement) {
      new bootstrap.Modal(modalElement).show(); // Inicializa e exibe o modal
    }
  }
  closeModal(id:string): void {
    const modalElement = document.getElementById(id);
    if (modalElement) {
      const modal = bootstrap.Modal.getInstance(modalElement); // Obtém a instância do modal
      if (modal) {
        modal.hide(); // Fecha o modal
      }
    }
  }
}
