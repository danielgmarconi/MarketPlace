import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../models/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MethodResponse } from '../models/method-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url : string = environment.webapi + '/User'
  constructor(private http: HttpClient) { }
  get(model : User): Observable<MethodResponse> {
    let params = new HttpParams();
    if(model.id != undefined) params =  params.set('Id', model.id);
    if(model.fullName != undefined) params = params.set('fullName', model.fullName);
    if(model.email != undefined) params = params.set('email', model.email);
    if(model.status != undefined) params = params.set('status', model.status);
    if(model.isBlocked != undefined) params = params.set('isBlocked', model.isBlocked);
    return this.http.get<MethodResponse>(this.url, {params});
  }
}
