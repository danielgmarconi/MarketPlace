import { MethodResponse } from './../../models/method-response';
import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoadingService } from '../../services/loading.service';
import { AuthService } from '../../services/auth.service';
import { Authentication } from '../../models/authentication';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { IconType } from '../../shared/messagebox/icon-type';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  usuario: string = '';
  email: string = '';
  senha: string = '';
  fullName: string = '';
  forcaMensagem: string = '';
  forcaClasse: string = '';



  modalLogin : boolean = true;
  modalNewAccount : boolean = false;
  showModal = false;

  formLogin!: FormGroup;



  constructor(private authService: AuthService,
              private messageboxService : MessageboxService,
              private fb: FormBuilder
  ) {}

  ngOnInit() {

  }
  createFromGrupLogin()
  {
      this.formLogin = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }
  messageValidationLogin()
  {
    const lista: string[] = [];
    let msg : string = ''
    const ctrlEmail = this.formLogin.get('email');
    if (ctrlEmail && ctrlEmail.errors && ctrlEmail.errors['required']) lista.push('Campo Email obrigatório.');
    if (ctrlEmail && ctrlEmail.errors && ctrlEmail.errors['email']) lista.push('Email inválido.');
    const ctrlPassword = this.formLogin.get('password');
    if (ctrlPassword && ctrlPassword.errors && ctrlPassword.errors['required']) lista.push('Campo Senha obrigatório.');
    if (ctrlPassword && ctrlPassword.errors && ctrlPassword.errors['minlength']) lista.push('Tamanho da senha invalido.');

    for(let a=0; a<lista.length; a++)
      msg+= a!=0 ? ('<br>' + lista[a]) : lista[a];
    this.messageboxService.openModal('Atenção', msg, IconType.warning);
  }

  loginOpen()
  {
    this.showModal = true;
    this.modalLogin = true;
    this.modalNewAccount = false;
    this.createFromGrupLogin();
  }
  newAccountOpen()
  {
    this.showModal = true;
    this.modalLogin = false;
    this.modalNewAccount = true;
  }
  cancel()
  {
    this.showModal = false;
  }

  access(){
      if (!this.formLogin.valid)
      {
        this.messageValidationLogin();
        return;
      }
      let authentication: Authentication = this.formLogin.value;
      this.authService.login(authentication).subscribe({
        next: res => {
          this.formLogin.reset();
          this.showModal = false;
        },
        error: err => {
          if(err.status == 401)
            this.messageboxService.openModal('Atenção', 'Acesso não autorizado', IconType.danger);
          else if(err.status == 500)
          {
            let methodResponse:MethodResponse = err.error;
            this.messageboxService.openModal('Atenção', methodResponse.response, IconType.danger);
          }
          else
            this.messageboxService.openModal('Atenção', 'Erro interno', IconType.danger);
        }
      });
  }




    verificarForcaSenha() {
    const senha = this.senha;

    const temLetra = /[a-zA-Z]/.test(senha);
    const temNumero = /[0-9]/.test(senha);
    const temEspecial = /[!@#$%^&*(),.?":{}|<>]/.test(senha);

    if (senha.length < 6) {
      this.forcaMensagem = 'Senha fraca';
      this.forcaClasse = 'fraca';
    } else if (temLetra && temNumero && !temEspecial) {
      this.forcaMensagem = 'Senha média';
      this.forcaClasse = 'media';
    } else if (temLetra && temNumero && temEspecial) {
      this.forcaMensagem = 'Senha forte';
      this.forcaClasse = 'forte';
    } else {
      this.forcaMensagem = 'Senha fraca';
      this.forcaClasse = 'fraca';
    }
  }

}
