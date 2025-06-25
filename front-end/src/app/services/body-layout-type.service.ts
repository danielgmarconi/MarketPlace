import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
// Enum fora da classe
export enum TypeBody {
  Default = 'D',
  Profile = 'P'
}
@Injectable({
  providedIn: 'root'
})
export class BodyLayoutTypeService {
  private key = 'typeBody';
  constructor(private authService: AuthService) { }
  setBodyLayout(type:TypeBody){
    localStorage.setItem(this.key, type.valueOf());
  }
  getBodyLayout(){
    if(this.authService.isTokenExpired())
      localStorage.removeItem(this.key);
     return localStorage.getItem(this.key) ?? 'D';
  }
  reset()
  {
    localStorage.removeItem(this.key);
  }
}
