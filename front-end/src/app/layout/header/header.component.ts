import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-header',
    imports: [RouterLink, LoginComponent],
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
   @ViewChild('loginComponent') loginComponent!: LoginComponent;
   constructor(public authService: AuthService){}
  ngOnInit(): void {
    //alert(this.authService.isAuthenticated());
  }

  loginOpen()
  {
    this.loginComponent.loginOpen()
  }
  newAccountOpen()
  {
    this.loginComponent.newAccountOpen()
  }
  helpOpen()
  {

  }
}
