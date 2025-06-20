import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  usuario: string = '';
  email: string = '';
  senha: string = '';
  fullName: string = '';
  forcaMensagem: string = '';
  forcaClasse: string = '';
  public xxx : boolean = true;
  @ViewChild('formLogin') form!: NgForm;

  constructor(private loadingService: LoadingService) {}



  onLogin() {
    console.log('Usuário:', this.usuario);
    console.log('Senha:', this.senha);
    // Aqui você pode chamar um serviço de autenticação
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
    submitForm() {
      this.loadingService.show();
      this.xxx = false;
      setTimeout(() => {
      this.loadingService.hide();
      this.xxx = true;
      }, 2000);

    // if (this.form.invalid) {
    //   Object.values(this.form.controls).forEach(control => control.markAsTouched());
    //   alert('Formulário inválido!');
    //   return;
    // }
    // Object.entries(this.form.controls).forEach(([name, control]) => {
    //   control.markAsTouched();
    //   console.log('Campo:', name, '| Valor:', control.value);
    // });
//     console.log('Formulário enviado com sucesso!');
//     console.log('Usuário:', this.usuario);
//     //console.log('Email:', this.email);
//     console.log('Senha:', this.senha);
  }
}
