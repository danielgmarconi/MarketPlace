import { Component } from '@angular/core';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { CommonModule } from '@angular/common';
import { ModalContentService } from '../../services/modal-content.service';
import { IconType } from '../../shared/messagebox/icon-type';

@Component({
  selector: 'app-teste',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './teste.component.html',
  styleUrl: './teste.component.scss'
})
export class TesteComponent {
  constructor(private MessageboxService: MessageboxService, private modalcontentService : ModalContentService) {}

  xxx : string = 'a';
  openModal(id:string): void {
    this.modalcontentService.openModal(id);
    // const modalElement = document.getElementById('exampleModal');
    // if (modalElement) {
    //   new bootstrap.Modal(modalElement).show(); // Inicializa e exibe o modal
    // }
  }
  closeModal(id:string): void {
    this.modalcontentService. closeModal(id);
    // const modalElement = document.getElementById('exampleModal');
    // if (modalElement) {
    //   const modal = bootstrap.Modal.getInstance(modalElement); // Obtém a instância do modal
    //   if (modal) {
    //     modal.hide(); // Fecha o modal
    //   }
    // }
  }
  x2()
  {
   this.MessageboxService.openModal(this.xxx, this.xxx,  IconType.danger);
  }
  x(){

    this.MessageboxService.openModal(this.xxx, this.xxx,  IconType.danger);
    this.xxx += 'a';
  }
}
