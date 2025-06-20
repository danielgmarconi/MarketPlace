import { Component, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-footer',
    imports: [],
    templateUrl: './footer.component.html',
    styleUrl: './footer.component.scss'
})
export class FooterComponent {
  @Output('closeSidebar') closeSidebar = new EventEmitter<MouseEvent>();
  _closeSidebar(event: MouseEvent): void {
    this.closeSidebar.emit(event);
  }
}
