import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { AuthService } from '../../services/auth.service';
import { BodyLayoutTypeService, TypeBody } from '../../services/body-layout-type.service';
declare var bootstrap: any;

@Component({
    selector: 'app-header',
    imports: [RouterLink, LoginComponent],
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
   constructor(public authService: AuthService,
               private bodyLayoutTypeService : BodyLayoutTypeService){}
  @ViewChild('userDropdown', { static: false }) dropdownButton!: ElementRef;
  @ViewChild('loginComponent') loginComponent!: LoginComponent;
  @ViewChild('navbarContent', { static: false }) navbarCollapse!: ElementRef;
  private dropdownInstance: any;
    toggleDropdown() {
    if (!this.dropdownInstance) {
      this.dropdownInstance = new bootstrap.Dropdown(this.dropdownButton.nativeElement);
    }
    this.dropdownInstance.toggle();
  }
  toggleNavbar() {
    const collapseEl = this.navbarCollapse.nativeElement;
    const collapseInstance = bootstrap.Collapse.getInstance(collapseEl)
      ?? new bootstrap.Collapse(collapseEl);
    collapseInstance.toggle();
  }
  closeNavbar() {
  const bsCollapse = bootstrap.Collapse.getInstance(this.navbarCollapse.nativeElement);
  if (bsCollapse) {
    bsCollapse.hide();
  }
  }
  ngOnInit(): void {
    //alert(this.authService.isAuthenticated());
  }

  loginOpen()
  {
    this.loginComponent.loginOpen()
    this.closeNavbar();
  }
  newAccountOpen()
  {
    this.loginComponent.newAccountOpen()
  }
  helpOpen()
  {

  }
  Profile()
  {
    this.bodyLayoutTypeService.setBodyLayout(TypeBody.Profile);
  }
  logout()
  {
    this.authService.logout();
    this.bodyLayoutTypeService.reset();
  }
}
