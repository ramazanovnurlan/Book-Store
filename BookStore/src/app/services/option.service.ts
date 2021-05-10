import { HttpClient,HttpParams  } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { AppConfig } from "../app.config";
import { MessageDialog } from "../dialog/messageDialog/message-dialog";
import { BaseService } from "./base.service";
import { LanguageService } from "../language-service.service";

@Injectable({
    providedIn: 'root',
  })

  export class OptionService extends BaseService {
    pageParam!: number;
    pageIndex:any;
    change:any;
    myAppUrl: any;

    constructor(
      private http: HttpClient,private router: Router,private langService: LanguageService,private dialog: MatDialog) {
      super();
      
    }

    setChange(change:any){
      this.change=change;
    }
    getChange(): any {
      return this.change;
    }
    setPageIndex(index:any){
      this.pageIndex=index;
    }
    getPageIndex(): any {
       return this.pageIndex;
    }

    getAuthorList(lang:any):Observable<any>{
        this.myAppUrl = AppConfig.settings.other;
        let url = this.myAppUrl["resourceApiURI_"+ lang] + "/author/author-list";
        return this.http.get<any>(url);
    }
    getCategoryList(lang:any):Observable<any>{
        this.myAppUrl = AppConfig.settings.other;
        let url = this.myAppUrl["resourceApiURI_"+ lang] + "/category/category-list";
        return this.http.get<any>(url);
    }
    getLanguageList(lang:any):Observable<any>{
        this.myAppUrl = AppConfig.settings.other;
        let url = this.myAppUrl["resourceApiURI_"+ lang] + "/language/language-list";
        return this.http.get<any>(url);
    }
    deleteAuthor(id:any,lang:any){
      this.myAppUrl = AppConfig.settings.other;
      let url = this.myAppUrl["resourceApiURI_"+ lang] + "/author/delete-author/"+id;
      return this.http.get<any>(url);
    }
    deleteCategory(id:any,lang:any){
      this.myAppUrl = AppConfig.settings.other;
      let url = this.myAppUrl["resourceApiURI_"+ lang] + "/category/delete-category/"+id;
      return this.http.get<any>(url);
    }
    deleteLanguage(id:any,lang:any){
      this.myAppUrl = AppConfig.settings.other;
      let url = this.myAppUrl["resourceApiURI_"+ lang] + "/language/delete-language/"+id;
      return this.http.get<any>(url);
    }
    newAuthor(obj:any,lang:any){
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ lang] + "/author/add-author";
     return this.http.post<any>(url,obj);
    }

    newCategory(obj:any,lang:any){
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ lang] + "/category/add-category";
     return this.http.post<any>(url,obj);
    }

    newLanguage(obj:any,lang:any){
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ lang] + "/language/add-language";
     return this.http.post<any>(url,obj);
    }

    getAuthorDetail(id:any):Observable<any>{
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ this.langService.getLang().toUpperCase()] + "/author/author-detail/"+id;
     return this.http.get<any>(url);
    }
    editAuthor(obj:any,lang:any,id:any){
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ lang] + "/author/edit-author/"+id;
     return this.http.post<any>(url,obj);
    }

    getCategoryDetail(id:any):Observable<any>{
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ this.langService.getLang().toUpperCase()] + "/category/category-detail/"+id;
     return this.http.get<any>(url);
    }
    editCategory(obj:any,lang:any,id:any){
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ lang] + "/category/edit-category/"+id;
     return this.http.post<any>(url,obj);
    }

    getLanguageDetail(id:any):Observable<any>{
      this.myAppUrl = AppConfig.settings.other;
      let url = this.myAppUrl["resourceApiURI_"+ this.langService.getLang().toUpperCase()] + "/language/language-detail/"+id;
      return this.http.get<any>(url);
    }
    editLanguage(obj:any,lang:any,id:any){
     this.myAppUrl = AppConfig.settings.other;
     let url = this.myAppUrl["resourceApiURI_"+ lang] + "/language/edit-language/"+id;
     return this.http.post<any>(url,obj);
    }

    
      

    public messageDialog(text: string, isRefresh: boolean) {

        this.dialog.open(MessageDialog, {
          width: '500px',
          position: {
            top: '10px',
          },
          data: { Text: `${text}`, isRefresh: isRefresh },
          autoFocus: false,
        });
      }

}