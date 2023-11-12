import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router) {

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    var accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
      var decodedToken: any = jwt_decode(accessToken);
      if (decodedToken.exp < Math.floor(Date.now() / 1000)) {
        this.router.navigate(["/auth/login"]);
        return false;
      }
      return true;
    }
    this.router.navigate(["/auth/login"]);
    return false;
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    var accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
      var decodedToken: any = jwt_decode(accessToken);
      if (decodedToken.exp < Math.floor(Date.now() / 1000)) {
        this.router.navigate(["/auth/login"]);
        return false;
      }
      return true;
    }
    this.router.navigate(["/auth/login"]);
    return false;
  }
}

