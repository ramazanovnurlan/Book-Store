import { Component, OnInit,ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { LanguageService } from '../language-service.service';
import { ProcessService } from '../services/process.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.scss']
})
export class BookDetailComponent implements OnInit {
  id!:number;
  currentbook:any;
  constructor(public authService: AuthService, public router: Router, public activatedRoute: ActivatedRoute, public langService: LanguageService, public processService: ProcessService) { 
    if(!authService.isLogin()){
      this.router.navigate(["/login"]);
    }
   //console.log(this.activatedRoute.snapshot.queryParams.id)
   this.activatedRoute.params.subscribe((params: Params) => {
    this.id = params['id'];
    this.getData();
    
  });

  }

  ngOnInit(): void {
  }

  getData(){
    this.processService.getBookDetail(this.id).subscribe(data => {
      if (data.errorCode && data.errorCode == 1) {
        
      } else {
        this.currentbook=data;
      }
    });
  }

  

}
