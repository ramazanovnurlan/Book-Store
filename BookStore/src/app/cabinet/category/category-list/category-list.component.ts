import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LanguageService } from '../../../language-service.service';
import { ProcessService } from '../../../services/process.service';
import { OptionService } from '../../../services/option.service';
import { PageModel } from '../../../models/PageModel.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {

  @ViewChild(MatSort)
  sort!: MatSort;
  @ViewChild(MatPaginator, {})
  paginatorServer!: MatPaginator;
  
  pageSize: number = 2;
  totalSize: number = 1;
  pageIndex: any ;
    
  searchKey: any;
  filterlist: PageModel = { skip: 0, limit: 2};
  displayedColumns: string[] = ['id','title','op','actions'];

  constructor(private fb: FormBuilder, public authService: AuthService, public router: Router, public langService: LanguageService, public optionService: OptionService) { 
    if(!authService.isLogin()){
      this.router.navigate(["/login"]);
    }
  }
  listData!: MatTableDataSource<any>;

  ngOnInit(): void {
    
    
    this.getData();
    
     var a=this.optionService.getChange();
     if(a=="deyisdi"){
       this.pageIndex=this.optionService.getPageIndex();
       console.log("pageIndex"+this.pageIndex);
       this.returnAgain();
     }
  }

  returnAgain(){
    this.optionService.getCategoryList(this.langService.getLang().toUpperCase()).subscribe(data => {
    this.listData=new MatTableDataSource(data);
    this.listData.paginator=this.paginatorServer;
    this.listData.paginator.pageIndex=this.pageIndex;
    this.listData.paginator=this.paginatorServer;
  });
  
}

getData(){
  this.optionService.getCategoryList(this.langService.getLang().toUpperCase()).subscribe(data => {
    if (data.errorCode && data.errorCode == 1) {
      this.optionService.messageDialog(data.errorMessage, false);
    } else {
     this.listData=new MatTableDataSource(data);
     this.listData.sort=this.sort;
     this.listData.paginator=this.paginatorServer;
      console.log(this.listData.paginator);
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
 
  this.getData();
}
goto_edit(id:number){
  this.optionService.setPageIndex(this.paginatorServer.pageIndex.toString());
  this.optionService.setChange("deyisdi");
  this.router.navigate(["/cabinet/category-edit/"+id]);
}
deleteCategory(id:number){
  this.optionService.deleteCategory(id,this.langService.getLang().toUpperCase()).subscribe(data => {
    if (data.errorCode && data.errorCode == 1) {
      this.optionService.messageDialog(data.result, false);
    } else {
     
      this.optionService.messageDialog(data.result, false);
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
  this.router.navigate(["/cabinet/new-category"]);
}

}
