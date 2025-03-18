import { Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Output('closeSidebar') closeSidebar = new EventEmitter<MouseEvent>();
  @Output('toggleSidebar') toggleSidebar = new EventEmitter<void>();


  screenWidth!: number;

  // Atualiza a largura da tela ao redimensionar
  @HostListener('window:resize', ['$event'])
  onResize(): void {
    this.screenWidth = window.innerWidth;
  }

  ngOnInit(): void {
    // Obtém o tamanho inicial da tela

    this.screenWidth = window.innerWidth;
  }

  _closeSidebar(event: MouseEvent): void {

    this.closeSidebar.emit(event);
  }
  _toggleSidebar(): void {
    this.toggleSidebar.emit();
  }
}
