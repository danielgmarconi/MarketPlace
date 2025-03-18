import { Component } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  isSidebarVisible = false;
  public toggleSidebar(): void {
    this.isSidebarVisible = !this.isSidebarVisible;
  }
  public closeSidebar(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (this.isSidebarVisible && !target.closest('.sidebar') && !target.closest('button')) {
      this.isSidebarVisible = false;
    }
  }
}
