import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-addresses',
  imports: [CommonModule, FormsModule],
  templateUrl: './addresses.component.html',
  styleUrl: './addresses.component.scss'
})
export class AddressesComponent implements OnInit {
  items = [
    { id: 1, nome: 'Camiseta', categoria: 'Roupas', preco: 49.9 },
    { id: 2, nome: 'Notebook', categoria: 'Eletrônicos', preco: 2999.0 },
    { id: 3, nome: 'Livro Angular', categoria: 'Livros', preco: 79.9 },
    { id: 4, nome: 'Mouse', categoria: 'Acessórios', preco: 99.5 },
    { id: 1, nome: 'Camiseta', categoria: 'Roupas', preco: 49.9 },
    { id: 2, nome: 'Notebook', categoria: 'Eletrônicos', preco: 2999.0 },
    { id: 3, nome: 'Livro Angular', categoria: 'Livros', preco: 79.9 },
    { id: 4, nome: 'Mouse', categoria: 'Acessórios', preco: 99.5 },
    { id: 1, nome: 'Camiseta', categoria: 'Roupas', preco: 49.9 },
    { id: 2, nome: 'Notebook', categoria: 'Eletrônicos', preco: 2999.0 },
    { id: 3, nome: 'Livro Angular', categoria: 'Livros', preco: 79.9 },
    { id: 4, nome: 'Mouse', categoria: 'Acessórios', preco: 99.5 },
    { id: 1, nome: 'Camiseta', categoria: 'Roupas', preco: 49.9 },
    { id: 2, nome: 'Notebook', categoria: 'Eletrônicos', preco: 2999.0 },
    { id: 3, nome: 'Livro Angular', categoria: 'Livros', preco: 79.9 },
    { id: 4, nome: 'Mouse', categoria: 'Acessórios', preco: 99.5 },
    { id: 1, nome: 'Camiseta', categoria: 'Roupas', preco: 49.9 },
    { id: 2, nome: 'Notebook', categoria: 'Eletrônicos', preco: 2999.0 },
    { id: 3, nome: 'Livro Angular', categoria: 'Livros', preco: 79.9 },
    { id: 4, nome: 'Mouse', categoria: 'Acessórios', preco: 99.5 },
    { id: 1, nome: 'Camiseta', categoria: 'Roupas', preco: 49.9 },
    { id: 2, nome: 'Notebook', categoria: 'Eletrônicos', preco: 2999.0 },
    { id: 3, nome: 'Livro Angular', categoria: 'Livros', preco: 79.9 },
    { id: 4, nome: 'Mouse', categoria: 'Acessórios', preco: 99.5 },
    // ... adicione mais para testar paginação
  ];

  searchText: string = '';
  filteredItems: any[] = [];

  // Paginação
  currentPage: number = 1;
  pageSize: number = 10;
  paginatedItems: any[] = [];

  get totalPages(): number {
    return Math.ceil(this.filteredItems.length / this.pageSize);
  }

  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  ngOnInit(): void {
    this.filteredItems = this.items;
    this.updatePaginatedItems();
  }

  onSearch(): void {
    const query = this.searchText.trim().toLowerCase();
    this.filteredItems = this.items.filter(
      item =>
        item.nome.toLowerCase().includes(query) ||
        item.categoria.toLowerCase().includes(query)
    );
    this.currentPage = 1;
    this.updatePaginatedItems();
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePaginatedItems();
  }

  private updatePaginatedItems(): void {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedItems = this.filteredItems.slice(start, end);
  }
}
