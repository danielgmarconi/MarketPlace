import { AddressesComponent } from './pages/addresses/addresses.component';
import { HomeComponent } from './pages/home/home.component';
import { TesteComponent } from './pages/teste/teste.component';
;
import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home'
  },
  {
    path:'home',
    component:HomeComponent
  },
  {
    path:'teste',
    component:TesteComponent
  },
  {
    path:'addresses',
    component:AddressesComponent
  },
  {
    path: '***',
    redirectTo:'home'
  }
];
