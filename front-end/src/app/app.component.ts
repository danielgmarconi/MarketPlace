import { Component, HostListener, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { CommonModule } from '@angular/common';
import { MessageboxComponent } from './shared/messagebox/messagebox.component';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, HeaderComponent, FooterComponent, CommonModule, MessageboxComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Shopizo';
  @ViewChild('appSidebar') appSidebar!: SidebarComponent;


  constructor() {}
  toggleSidebar(): void {
    this.appSidebar.toggleSidebar();
  }

  closeSidebar(event: MouseEvent): void {
    this.appSidebar.closeSidebar(event);

  }
  ngAfterViewInit() {

  }
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    this.appSidebar.closeSidebar(event);
  }
}
