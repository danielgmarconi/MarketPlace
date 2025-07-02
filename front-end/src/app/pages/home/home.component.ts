import { Component } from '@angular/core';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { IconType } from '../../shared/messagebox/icon-type';
import { GridViewComponent, Setbutton, SetColumn } from '../../shared/grid-view/grid-view.component';
import { User } from '../../models/user';

@Component({
    selector: 'app-home',
    imports: [GridViewComponent],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(private messageboxService : MessageboxService){}
  users: User[] = [
  {
    id: 1,
    fullName: 'João da Silva',
    email: 'joao.silva@email.com',
    password: 'Senha@123',
    status: 'ativo',
    isBlocked: false
  },
  {
    id: 2,
    fullName: 'Maria Oliveira',
    email: 'maria.oliveira@email.com',
    password: 'Segura!2023',
    status: 'inativo',
    isBlocked: true
  },
  {
    id: 3,
    fullName: 'Carlos Souza',
    email: 'carlos.souza@email.com',
    password: 'Carlos$456',
    status: 'ativo',
    isBlocked: false
  },
  {
    id: 4,
    fullName: 'Ana Pereira',
    email: 'ana.pereira@email.com',
    password: 'AnaP@2024',
    status: 'pendente',
    isBlocked: false
  },
  {
    id: 5,
    fullName: 'Roberto Lima',
    email: 'roberto.lima@email.com',
    password: 'Lima123!',
    status: 'ativo',
    isBlocked: true
  }
  ];
  col: SetColumn[] = [ { propertyBinding: 'id', description: 'ID'}, { propertyBinding: 'fullName', description:'Nome completo'}];
  btn: Setbutton[] = [ new Setbutton((model) => this.xxx(model), 'teste', 'bi bi-trash'), new Setbutton((model) => this.xxx(model), 'teste', 'bi bi-trash'), new Setbutton((model) => this.xxx(model), 'teste', 'bi bi-trash')];

  teste()
  {
    this.messageboxService.openModal('a', 'b', IconType.danger);
  }
  xxx(v:User) : void
  {
    console.log(v);
  }
}
