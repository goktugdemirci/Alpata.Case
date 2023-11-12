import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }
  getToken() : string {
    var result = localStorage.getItem("accessToken");
    if(result) {
      return result;
    }
    return "";
  }
}
