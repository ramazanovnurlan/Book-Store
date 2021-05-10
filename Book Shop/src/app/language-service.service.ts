
import { Injectable, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  languageChanged = new Subject<string>();
  constructor() { }

  getLang(): string {
    return localStorage.getItem("lang") || "en";
  }
  
  setLang(lang: string) {
    if (lang) {
      localStorage.setItem("lang", lang);
      this.languageChanged.next(lang);
     // window.location.reload();
    }
  }
} 