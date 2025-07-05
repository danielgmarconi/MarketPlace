import { User } from './../../models/user';
import { Component, ViewChild } from '@angular/core';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { IconType } from '../../shared/messagebox/icon-type';
import { GridViewComponent, Setbutton, SetColumn } from '../../shared/grid-view/grid-view.component';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';
import { BehaviorSubject, delay } from 'rxjs';

import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ComboBoxComponent } from '../../shared/combo-box/combo-box.component';

@Component({
    selector: 'app-home',
    imports: [GridViewComponent, ComboBoxComponent, FormsModule, CommonModule, ReactiveFormsModule],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(private messageboxService : MessageboxService, private authService: AuthService, private userService: UserService, private fb: FormBuilder){}
  public users$ = new BehaviorSubject<User[]>([]);

  col: SetColumn[] = [ {propertyBinding: 'id', description: 'ID'}, {propertyBinding: 'fullName', description:'Nome completo'}, {propertyBinding: 'email', description:'E-Mail'}];
  btn: Setbutton[] = [ new Setbutton((model) => this.xxx(model), 'teste', 'bi bi-trash'), new Setbutton((model) => this.xxx(model), 'teste', 'bi bi-trash'), new Setbutton((model) => this.xxx(model), 'teste', 'bi bi-trash')];

  @ViewChild('teste11') selectComp!: ComboBoxComponent<User>;
  formTeste!: FormGroup;

  ngOnInit(): void {
      this.formTeste = this.fb.group({
      cbx: ['', [Validators.required]],
    });
  }

  teste()
  {
    //this.messageboxService.openModal('a', 'b', IconType.danger);
        if(this.authService.isAuthenticated())
    {
      let user : User = {};
      user.status = 'A';
      this.userService.get(user).subscribe({
      next: (res) => {
        if(res.statusCode =200)
        {
          this.users$.next(res.response);
    //             this.formTeste = this.fb.group({
    //   teste: ['', [Validators.required]],
    // });
        }

        //console.log('Resposta111:', res);
      },
      error: (err) => {
        console.error('Erro ao buscar usuários', err);
      }
    });
    }
  }
  xxx(v:User) : void
  {
    console.log(v);
  }
  valueChange(value: any) {
  console.log('Selecionado:', value);
  }
    teste1()
  {

    this.selectComp.setSelected('19');
    setTimeout(() => {
      console.log(this.formTeste.valid);
    });
    // this.selectComp.forcarSelecao(19);
    //console.log(this.formTeste.valid);
    //console.log('Valor selecionado:', this.formTeste.value.cbx);
    // this.selectComp.forcarSelecao('');
    //     console.log(this.formTeste.valid);
    // console.log('Valor selecionado:', this.formTeste.value.cbx);
  }
}
