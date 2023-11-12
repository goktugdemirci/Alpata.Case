import { Injectable } from '@angular/core';
import { BaseService } from './base/base.service';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  protected requestUrl: string = "";
  constructor(public http: HttpClient) { this.requestUrl = `${environment.apiEndPoint}/api/users`; }

  create(model: User) {
    return this.http.post<User>(this.requestUrl, model);
  }
  isUniqueEmail(email: string) {
    return this.http.get(`${this.requestUrl}/${email}/IsUniqueEmail`);
  }

  isUniquePhoneNumber(phoneNumber: string) {
    return this.http.get(`${this.requestUrl}/${phoneNumber}/IsUniquePhoneNumber`);
  }

  upload(formData:FormData){
    return this.http.post(`${this.requestUrl}/ProfilePhoto/Upload`,formData);
  }
}
