import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable } from "rxjs";
import { AppConfig } from "../app.config";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root',
})

/**
 * Provides helper methods to create routes.
 */
export class AuthService extends BaseService {
  myAppUrl: any;
  constructor(
    private http: HttpClient,
    private router: Router,
    private dialog: MatDialog
  ) {
    super();
  }
 
  
  login(obj:any, lang:string): Observable<any> {
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/auth/login";
    console.log(url);
    return this.http.post<any>(url, obj);
  }

  register(obj:any, lang:string): Observable<any> {
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/auth/register";
    console.log(url);
    return this.http.post<any>(url, obj);
  }

  // helper methods
  clearStorage() {
    if (!localStorage.getItem('token')) {
      this.router.navigate(['/login']);
    }
    var hours = AppConfig.settings.other.clearStorageHour;
    var now = new Date().getTime();
    var setupTime = localStorage.getItem('setupTime');
    if (setupTime == null) {
      localStorage.setItem('setupTime', now.toString());
    } else {
      if (now - parseInt(setupTime) > hours * 60 * 60 * 1000) {
        localStorage.clear();
        window.location.reload();
        localStorage.setItem('setupTime', now.toString());
      }
    }

    // if (!localStorage.getItem("token")) {
    //   this.router.navigate(["/login"]);
    // }
  }

   isLogin():boolean{
    //this.clearStorage();
     if(localStorage.getItem("token")){
      const helper = new JwtHelperService();
      var token:any=localStorage.getItem("token");
      const decodedToken = helper.decodeToken(token);
      if(decodedToken){
        return true;
      }
     }
     return false;
   }
 
  createJWT(token:any, returnToUrl:any) {
    localStorage.setItem("token", token.token);
    localStorage.setItem("tokenExpiration", token.expireAt);
    console.log(token);
    if (returnToUrl) {
      this.router.navigate([returnToUrl]);
    }
  }

  getUserName() {
    if (localStorage.getItem("token")) {
      const helper = new JwtHelperService();
      var token:any=localStorage.getItem("token");
      const decodedToken = helper.decodeToken(token);
      if (decodedToken && decodedToken["UserName"]) {
        return {username: decodedToken["UserName"], errorCode: 0};
      } else {
        return {username: null, errorCode: 1};
      }
    } else {
      return {username: null, errorCode: 1};
    }
  }

  signOut(){
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");
    this.router.navigate(["/login"]);
  }

  getToken() {
    return localStorage.getItem("token");
  }

}
