import { Component, OnInit } from '@angular/core';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
declare var $: any;
//declare function myFun():void; 
declare const myFun:any; //the same call method
  


@Component({
  selector: 'app-book-corousel',
  templateUrl: './book-corousel.component.html',
  styleUrls: ['./book-corousel.component.scss'],
  providers: [NgbCarouselConfig]
})
export class BookCorouselComponent implements OnInit {

  constructor( ) { 
     
  }
  ngOnInit(){
  
  }
  callfunc(){
    //myFun();
  }
}

