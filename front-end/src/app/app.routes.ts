import { authGuard } from './guard/auth.guard';
import { ActivationComponent } from './pages/activation/activation.component';
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
    canActivate: [authGuard],
    component:TesteComponent
  },
  {
    path:'addresses',
    component:AddressesComponent
  },
  {
    path:'activation/:guid',
    component: ActivationComponent
  },
  {
    path: '***',
    redirectTo:'home'
  }
];
