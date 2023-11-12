import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserLogin } from '../model/userLogin';
import { UserLoginResult } from '../model/userLoginResult';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiEndpoint: string;
  constructor(protected http: HttpClient) {
    this.apiEndpoint = environment.apiEndPoint;

  }
  authenticate(userLogin: UserLogin) {
    return this.http.post<UserLoginResult>(`${this.apiEndpoint}/api/Users/Authenticate`, userLogin);
  }




  
}
