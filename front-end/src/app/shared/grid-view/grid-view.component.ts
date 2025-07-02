import { CommonModule } from '@angular/common';
import { Component, input, Input, OnInit } from '@angular/core';
import * as bootstrap from 'bootstrap';
export class SetColumn {
  propertyBinding!: string;
  description!:string;
}
export class Setbutton {
  toolTip!:string;
  iconClass!:string;
  buttonClass!:string;
  action?: (model: any) => void
  constructor(
    action?: (model: any) => void,
    toolTip: string = '',
    iconClass: string = 'bi bi-question-diamond-fill',
    buttonClass: string = '',
  ) {
    this.action = action
    this.toolTip = toolTip;
    this.iconClass = iconClass;
    this.buttonClass = buttonClass;
  }
}
@Component({
  selector: 'app-grid-view',
  imports: [CommonModule],
  templateUrl: './grid-view.component.html',
  styleUrl: './grid-view.component.scss'
})
export class GridViewComponent<T> implements OnInit {
  @Input() columns: SetColumn[] = []
  @Input() buttons: Setbutton[] = []
  @Input() data: T[] = [];
  @Input() pageSize: number = 10;
  @Input() buttonClassDefault: string = 'btn btn-secondary';
  currentPage: number = 1;
  paginatedItems: T[] = [];
  ngOnInit(): void {
    this.updatePaginatedItems();
  }
  getProp(obj: any, key: string): any {
    return obj[key];
  }
  ngAfterViewInit() : void {
    const el = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    el.forEach(e => new bootstrap.Tooltip(e));
  }

    get totalPages(): number {
    return Math.ceil(this.data.length / this.pageSize);
  }
  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
    goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePaginatedItems();
  }
  private updatePaginatedItems(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedItems = this.data.slice(start, end);
  }
}
