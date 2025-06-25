import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar-profile',
  imports: [CommonModule,RouterLink],
  templateUrl: './sidebar-profile.component.html',
  styleUrl: './sidebar-profile.component.scss'
})
export class SidebarProfileComponent {
  menuItems = [
    { label: 'Dashboard', icon: 'bi bi-house', route: '/dashboard' },
    { label: 'Usuários', icon: 'bi bi-people', route: '/usuarios' },
    { label: 'Relatórios', icon: 'bi bi-file-earmark-bar-graph', route: '/relatorios' },
    { label: 'Configurações', icon: 'bi bi-gear', route: '/configuracoes' }
  ];
}
