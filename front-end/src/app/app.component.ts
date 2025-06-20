import { Component, HostListener, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { CommonModule } from '@angular/common';
import { MessageboxComponent } from './shared/messagebox/messagebox.component';
import { LoadingComponent } from './shared/loading/loading.component';



@Component({
    selector: 'app-root',
    imports: [RouterOutlet, HeaderComponent, FooterComponent, CommonModule, MessageboxComponent, LoadingComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Shopizo';


  constructor() {}


  ngAfterViewInit() {

  }
}
