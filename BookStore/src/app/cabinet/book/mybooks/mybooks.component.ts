import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LanguageService } from '../../../language-service.service';
import { ProcessService } from '../../../services/process.service';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
//import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
declare const myalfa:any;
import { PageModel } from '../../../models/PageModel.model';

@Component({
  selector: 'app-mybooks',
  templateUrl: './mybooks.component.html',
  styleUrls: ['./mybooks.component.scss']
})
export class MybooksComponent implements OnInit {

  @ViewChild(MatSort)
  sort!: MatSort;
  @ViewChild(MatPaginator, {})
  paginatorServer!: MatPaginator;
  ///
  
  ///
  pageSize: number = 2;
  totalSize: number = 1;
  pageIndex: any ;
  ///
  searchKey: any;
  filterlist: PageModel = { skip: 0, limit: 2};
  displayedColumns: string[] = ['id','title','description','price','op','actions'];
  constructor(private fb: FormBuilder, public authService: AuthService, public router: Router, public langService: LanguageService, public processService: ProcessService) { 
    if(!authService.isLogin()){
      this.router.navigate(["/login"]);
    }
  }
  bookDBList !: MatTableDataSource<any>;
  listData!: MatTableDataSource<any>;
  ngOnInit(): void {
    
    
    this.getData();
    // if (this.bookDBList.paginator) {
    //   this.bookDBList.paginator.firstPage();
    // }
     var a=this.processService.getChange();
     if(a=="deyisdi"){
       console.log("aaaaaaaaaaaaaaaaa");
       this.pageIndex=this.processService.getPageIndex();
       console.log("pageIndex"+this.pageIndex);
       this.returnAgain();
     }
  }
  returnAgain(){
      this.processService.getBookList(this.langService.getLang().toUpperCase()).subscribe(data => {
      this.listData=new MatTableDataSource(data);
      this.listData.paginator=this.paginatorServer;
      this.listData.paginator.pageIndex=this.pageIndex;
      this.listData.paginator=this.paginatorServer;
    });
    
  }

  getData(){
    this.processService.getBookList(this.langService.getLang().toUpperCase()).subscribe(data => {
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.errorMessage, false);
      } else {
       this.listData=new MatTableDataSource(data);
       this.listData.sort=this.sort;
       
       this.listData.paginator=this.paginatorServer;
       
       
       ///////////////////////////////////
       //this.listData.paginator.pageIndex=4;
      //  this.listData.paginator=this.paginatorServer;
       //////////////////////
      //  console.log(this.listData);
        console.log(this.listData.paginator);
       
        this.bookDBList = new MatTableDataSource(data);
        this.bookDBList.sort;
        this.bookDBList.paginator=this.paginatorServer;
        this.totalSize = data.length;

         if(this.totalSize<parseInt(this.filterlist.skip)){
          this.paginatorServer.firstPage();
        }
       
      }
    });  
  }
  pageChanged(event: any) {
    let pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.filterlist.skip = pageIndex * this.pageSize;
    this.filterlist.limit =this.pageSize.toString();
    this.pageIndex=event.pageIndex;
    // console.log("pageIndex"+pageIndex)
    // console.log("pageSize"+this.pageSize)
    // console.log("this.filterlist.skip"+this.filterlist.skip)
    // console.log("this.filterlist.limit"+this.filterlist.limit)
    this.getData();
  }
  goto_edit(id:number){
    this.processService.setPageIndex(this.paginatorServer.pageIndex.toString());
    this.processService.setChange("deyisdi");
    this.router.navigate(["/cabinet/edit-book/"+id]);
  }
  deleteBook(id:number){
    this.processService.deleteBook(id,this.langService.getLang().toUpperCase()).subscribe(data => {
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.result, false);
      } else {
       
        this.processService.messageDialog(data.result, false);
      }
    }); 
  }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

  clickCreate(){
    this.router.navigate(["/cabinet/new-book"]);
  }
 

}
