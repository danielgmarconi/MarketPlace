import { MethodResponse } from '../../models/method-response';
import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoadingService } from '../../services/loading.service';
import { AuthService } from '../../services/auth.service';
import { Authentication } from '../../models/authentication';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { IconType } from '../../shared/messagebox/icon-type';
import { BodyLayoutTypeService, TypeBody } from '../../services/body-layout-type.service';
import { CustomValidatorsFormBuilder } from '../../shared/validators/custom-validators-form-builder';
import { User } from '../../models/user';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{

  fullName: string = '';
  forcaMensagem: string = '';
  forcaClasse: string = '';
  eMail: string = '';
  modalType: string = '';
  formLogin!: FormGroup;
  formNewAccount!: FormGroup;



  constructor(private route: ActivatedRoute,
              private router: Router,
              private authService: AuthService,
              private messageboxService : MessageboxService,
              public bodyLayoutTypeService: BodyLayoutTypeService,
              private fb: FormBuilder
  ) {}

  ngOnInit() {

    const action = this.route.snapshot.paramMap.get('action');

    if(action == 'access')
      this.loginOpen();
    else if(action == 'newaccount')
      this.newAccountOpen();
    else if(action == 'lostpassword')
    {

    }
    else if(action == 'changepassword')
    {

    }
  }
  createFromGrupLogin()
  {
      this.formLogin = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }
  createFromGrupNewAccount()
  {
      this.formNewAccount = this.fb.group({
      fullName: ['', [Validators.required, CustomValidatorsFormBuilder.fullName()]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8),CustomValidatorsFormBuilder.password()]]
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
  messageValidationNewAccount()
  {
    const lista: string[] = [];
    let msg : string = ''
    const ctrlFullName = this.formNewAccount.get('fullName');
    if (ctrlFullName && ctrlFullName.errors && ctrlFullName.errors['required']) lista.push('Campo Nome Completo obrigatório.');
    if (ctrlFullName && ctrlFullName.errors && ctrlFullName.errors['fullNameInvalido']) lista.push('Nome Completo inválido.');
    const ctrlEmail = this.formNewAccount.get('email');
    if (ctrlEmail && ctrlEmail.errors && ctrlEmail.errors['required']) lista.push('Campo Email obrigatório.');
    if (ctrlEmail && ctrlEmail.errors && ctrlEmail.errors['email']) lista.push('Email inválido.');
    const ctrlPassword = this.formNewAccount.get('password');
    if (ctrlPassword && ctrlPassword.errors && ctrlPassword.errors['required']) lista.push('Campo Senha obrigatório.');
    if (ctrlPassword && ctrlPassword.errors && ctrlPassword.errors['minlength']) lista.push('Tamanho da senha invalido.');
    if (ctrlPassword && ctrlPassword.errors && ctrlPassword.errors['password']) lista.push('Esta senha não atende os critérios mínimos que são caracteres maiúsculo, minusculo, numeros e caracteres especiais ');

    for(let a=0; a<lista.length; a++)
      msg+= a!=0 ? ('<br>' + lista[a]) : lista[a];
    this.messageboxService.openModal('Atenção', msg, IconType.warning);
  }
  loginOpen()
  {
    this.modalType = 'A';
    this.createFromGrupLogin();
  }
  newAccountOpen()
  {
    this.modalType = 'N';
    this.createFromGrupNewAccount();
  }
  //   newAccountOpen()
  // {
  //   this.modalType = 'N';
  //   this.createFromGrupNewAccount();
  // }
  cancel()
  {
    this.router.navigate(['/home']);
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
          this.bodyLayoutTypeService.setBodyLayout(TypeBody.Default);
        },
        error: err => {
          if(err.status == 401)
            this.messageboxService.openModal('Atenção', 'Acesso não autorizado', IconType.danger);
          else if(err.status >= 500)
          {
            if(err.status == 501)
              this.messageboxService.openModal('Atenção', "Conta não registrada.", IconType.info);
            else if(err.status == 502)
              this.messageboxService.openModal('Atenção', "Confirme o email de ativação.", IconType.info);
            else if(err.status == 503)
              this.messageboxService.openModal('Atenção', "Conta bloqueada.", IconType.info);
            else if(err.status == 504)
              this.messageboxService.openModal('Atenção', "Conta bloqueada, necessita alteração de senha link no e-mail.", IconType.info);
            else
            {
              let methodResponse:MethodResponse = err.error;
              this.messageboxService.openModal('Atenção', methodResponse.response, IconType.danger);
            }
          }
          else
            this.messageboxService.openModal('Atenção', 'Erro interno', IconType.danger);
        }
      });
  }
  create(){
      if (!this.formNewAccount.valid)
      {
        this.messageValidationNewAccount();
        return;
      }
      let user: User = this.formNewAccount.value;
      this.authService.register(user).subscribe({
        next: (res) =>{
          this.messageboxService.openModal('Atenção', 'Usuario criado com sucesso.', IconType.success);
          this.formNewAccount.reset;
          this.loginOpen();
        },
        error: (err) => {
          if(err.status == 400)
            this.messageboxService.openModal('Atenção', 'Péssima requisição.', IconType.danger);
          else if(err.status == 500)
          {
            let methodResponse:MethodResponse = err.error;
            this.messageboxService.openModal('Atenção', methodResponse.response, IconType.danger);
          }
          else
            this.messageboxService.openModal('Atenção', 'Erro interno', IconType.danger);
        }
      })
  }
  emailExists(event: FocusEvent)
  {
    const obj = event.target as HTMLInputElement;
    if(obj.value.length > 0)
    {
      const regex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
      if(regex.test(obj.value))
      {
        this.authService.emailExists(obj.value).subscribe({
          next: (data : MethodResponse) => {
            if(data.response == true)
            {
              this.messageboxService.openModal('Atenção', 'Este ' + obj.value + ' já esta cadastrado.', IconType.warning);
              obj.value = ''
            }
          },
          error: () =>{
            this.messageboxService.openModal('Atenção', 'Erro na validação do e-mail', IconType.danger);
            obj.value = ''
          }
        });
      }
    }
  }
  verificarForcaSenha(event: Event) {
    const senha = (event.target as HTMLInputElement).value;
    const temLetra = /[a-zA-Z]/.test(senha);
    const temNumero = /[0-9]/.test(senha);
    const temEspecial = /[!@#$%^&*(),.?":{}|<>]/.test(senha);
    if (senha.length < 8) {
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
