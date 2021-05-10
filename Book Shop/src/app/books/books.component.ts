import { Component, OnInit,ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LanguageService } from '../language-service.service';
import { ProcessService } from '../services/process.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { PageModel } from '../models/PageModel.model';
import {IProduct} from '../models/book.model';
import {BookParam} from '../models/book.Params';
import { timer } from 'rxjs';
import { timeInterval } from 'rxjs/operators';
import {IJust} from '../models/just.model';
import {FilterOption} from '../models/filerOption.model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})

export class BooksComponent implements OnInit {
  @ViewChild(MatSort)
  sort!: MatSort;
  @ViewChild(MatPaginator, {})
  paginatorServer!: MatPaginator;
  ///
  
  ///
  pageSize: number = 2;
  totalSize: number = 1;
  ///
  products!: IProduct[];
  count: any;
  //////////////////////////////////////////////
  //////////////////////////////////////////////
  products2!: IProduct[];

    // pager object
    pager: any = {};

    // paged items
    // paged items
  pagedItems!: any[];
    ////////////////////////////
  authorList!: any[];
  categoryList!: any[];
  languageList!: any[];

  selectedauthors: any[] = [];
  selectedcategories: any[] = [];
  selectedlangs: any[] = [];
  filterOption!:FilterOption;
  constructor( public authService: AuthService, public router: Router, public langService: LanguageService, public processService: ProcessService) { 
    if(!authService.isLogin()){
      this.router.navigate(["/login"]);
    }
    
  }
 

  ngOnInit(): void {
    
    this.getData();
    this.setPage(1);
    var a=this.processService.getChange();
     if(a=="deyisdi"){
       console.log("aaaaaaaaaaaaaaaaa");
       var currentPage=this.processService.getPage();
       this.setPage(currentPage);
       
     }
    this.selectAuthorTypes();
    this.selectCategoryTypes();
    this.selectLanguageTypes();
    console.log(this.authorList);
  }
  

  getData(){
    this.processService.getBookList2(this.langService.getLang().toUpperCase()).subscribe(data => {
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.errorMessage, false);
      } else {
       //this.products=data;
       this.products2=data;
       //console.log(this.products);
       
       
      }
    });  
  }
  setPage(page: number){
      
      this.processService.getCount(this.langService.getLang().toUpperCase()).subscribe(count => {
      this.pager = this.processService.getPager(count, page);
      //console.log(this.pager);
      // this.processService.setChange("deyisdi");
      setTimeout(() => {
        
        this.filterOption=new FilterOption();
        this.filterOption.authors=this.selectedauthors;
        this.filterOption.categories=this.selectedcategories;
        this.filterOption.languages=this.selectedlangs;
        console.log(this.filterOption);
        if(this.selectedauthors.length!=0 || this.selectedcategories.length!=0 || this.selectedlangs.length!=0){
         
          this.processService.filterBook(this.filterOption,this.langService.getLang().toUpperCase()).subscribe(data=>{
            
            this.products2=data;
            console.log(data);
            this.products=this.products2.slice(this.pager.startIndex, this.pager.endIndex + 1);
            console.log(this.products);
          });
        }
        if(this.selectedauthors.length==0 && this.selectedcategories.length==0 && this.selectedlangs.length==0){
          this.processService.getBookList(this.langService.getLang().toUpperCase()).subscribe(data=>{
            
            this.products2=data;
            this.products=this.products2.slice(this.pager.startIndex, this.pager.endIndex + 1);
          });
        }
        this.products = this.products2.slice(this.pager.startIndex, this.pager.endIndex + 1);
        
    }, 200);
      
    });  
    

  }

  clickFunction(id:number){
    this.processService.setPage(this.pager.currentPage);
    this.processService.setChange("deyisdi");
    //console.log(this.processService.getPage());
    this.router.navigate(["books/"+id]);
  }

  selectCategoryTypes(){
    this.processService.getCategoryList(this.langService.getLang().toUpperCase()).subscribe(data => {
      
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.errorMessage, false);
      } else {
        this.categoryList = data; 
      }

    });
  }
  selectAuthorTypes(){
    this.processService.getAuthorList(this.langService.getLang().toUpperCase()).subscribe(data => {
      
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.errorMessage, false);
      } else {
        this.authorList = data;
         this.count=data.length;
      }

    });
  }
  selectLanguageTypes(){
    this.processService.getLanguageList(this.langService.getLang().toUpperCase()).subscribe(data => {
      
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.errorMessage, false);
      } else {
        this.languageList = data; 
      }

    });
  }

  onClickauthor(id:number)
  {   
    let isExit=true;
    if(this.selectedauthors.length===0){
         this.selectedauthors.push(id); 
    }
    
    else{
      for (let index = 0; index < this.selectedauthors.length; index++) {
        if(this.selectedauthors[index]===id){
          this.selectedauthors.splice(index,1);
          isExit=false;
        }
        
      }
      if(isExit){
        this.selectedauthors.push(id);
        isExit=false;
      }
    }
    
    var currentPage=this.processService.getPage();
    this.setPage(currentPage);
    
  }

  onClickcategory(id:number){
    let isExit=true;
    if(this.selectedcategories.length===0){
         this.selectedcategories.push(id); 
    }
    
    else{
      for (let index = 0; index < this.selectedcategories.length; index++) {
        if(this.selectedcategories[index]===id){
          this.selectedcategories.splice(index,1);
          isExit=false;
        }
        
      }
      if(isExit){
        this.selectedcategories.push(id);
        isExit=false;
      }
    }
    var currentPage=this.processService.getPage();
    this.setPage(currentPage);
    
    
  }


  onClicklang(id:number){
    let isExit=true;
    if(this.selectedlangs.length===0){
         this.selectedlangs.push(id); 
    }
    
    else{
      for (let index = 0; index < this.selectedlangs.length; index++) {
        if(this.selectedlangs[index]===id){
          this.selectedlangs.splice(index,1);
          isExit=false;
        }
        
      }
      if(isExit){
        this.selectedlangs.push(id);
        isExit=false;
      }
    }
    var currentPage=this.processService.getPage();
    this.setPage(currentPage);
    
    
  }

}
