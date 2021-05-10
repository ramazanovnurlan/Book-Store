import { Component } from "@angular/core";
import { BreakpointObserver, Breakpoints } from "@angular/cdk/layout";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../services/auth.service';
import { LanguageService } from 'src/app/language-service.service';
import {ProcessService} from '../services/process.service';

declare const myFunction:any;
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']

})
export class MenuComponent {
  constructor(public processService: ProcessService,public authService: AuthService,private translate: TranslateService, private langService: LanguageService) {
    translate.setDefaultLang(this.langService.getLang().toLowerCase());
   }
   useLanguage(language: string) {
    this.langService.setLang(language);
    this.translate.use(this.langService.getLang().toLowerCase());
  }
  lang(){
     return this.langService.getLang();
  }
  getUsername() {
    return this.authService.getUserName();
  }
  isLogin():boolean{
    if(this.authService.isLogin()){
      return true
    }
    else{return false}
  }
  
  signOut() {
    return this.authService.signOut();
  }

  myalfa(){
    myFunction();
  }
  onClickBook(){
    
  }
}