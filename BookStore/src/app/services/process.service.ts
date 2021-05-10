import { HttpClient,HttpParams  } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { AppConfig } from "../app.config";
import { MessageDialog } from "../dialog/messageDialog/message-dialog";
import { BaseService } from "./base.service";
import { LanguageService } from "../language-service.service";
import {IProduct} from '../models/book.model';
import {BookParam} from '../models/book.Params';
import { IPagination, Pagination } from '../models/pagination.model';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

/**
 * Provides helper methods to create routes.
 */
 export class ProcessService extends BaseService {

  products: IProduct[] = [];
  pagination = new Pagination();
  pageParam!: number;
  pageIndex:any;
  change:any;
  myAppUrl: any;
  asa:any;
  constructor(
    private http: HttpClient,
    private router: Router,
    private langService: LanguageService,
    private dialog: MatDialog
  ) {
    super();
  }
  setPage(id:any): void {
    this.pageParam = id;
  }
  getPage(): any {
    return this.pageParam;
  }
  setPageIndex(index:any){
    this.pageIndex=index;
    
  }
  getPageIndex(): any {
    return this.pageIndex;
  }

  setChange(change:any){
    this.change=change;
    
  }
  getChange(): any {
    return this.change;
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

  getBookList(lang:any):Observable<any>{
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/book-list";
    
    return this.http.get<any>(url);
  }
  getBookList2(lang:any):Observable<any>{
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/book-list";
    return this.http.get<any>(url);
    
  }
  getCount(lang:any):Observable<any>{
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/count";
    return this.http.get<any>(url);
  }

  getUserBookList(lang:any):Observable<any>{
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/userbook-list";
    return this.http.get<any>(url);
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

  getBookDetail(id:any):Observable<any>{
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ this.langService.getLang().toUpperCase()] + "/book/book-detail/"+id;
    
    return this.http.get<any>(url);
  }
  editBook(obj:any,lang:any,id:any){
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/edit-book/"+id;
    return this.http.post<any>(url,obj);
  }

  newBook(obj:any,lang:any){
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/new-book";
    return this.http.post<any>(url,obj);
  }
  deleteBook(id:any,lang:any){
    console.log("coming service");
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/book-delete/"+id;
    return this.http.get<any>(url);
  }
  getPager(totalItems: number, currentPage: number = 1, pageSize: number = 10){
      // calculate total pages
        let totalPages = Math.ceil(totalItems / pageSize);
        // ensure current page isn't out of range
        if (currentPage < 1) { 
            currentPage = 1; 
        } else if (currentPage > totalPages) { 
            currentPage = totalPages; 
        }
        
        let startPage: number, endPage: number;
        if (totalPages <= 10) {
            // less than 10 total pages so show all
            startPage = 1;
            endPage = totalPages;
        } else {
            // more than 10 total pages so calculate start and end pages
            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }

        // calculate start and end item indexes
        let startIndex = (currentPage - 1) * pageSize;
        let endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

        // create an array of pages to ng-repeat in the pager control
        let pages = Array.from(Array((endPage + 1) - startPage).keys()).map(i => startPage + i);

        // return object with all pager properties required by the view
        return {
            totalItems: totalItems,
            currentPage: currentPage,
            pageSize: pageSize,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };

  }

  filterBook(obj:any,lang:any):Observable<any>{
    console.log("service");
    this.myAppUrl = AppConfig.settings.other;
    let url = this.myAppUrl["resourceApiURI_"+ lang] + "/book/book-filterlist";
    return this.http.post<any>(url,obj);
    
  }
}