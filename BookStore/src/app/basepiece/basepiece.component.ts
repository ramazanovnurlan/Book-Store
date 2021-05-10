import { Component, OnInit } from '@angular/core';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap'; 

@Component({
  selector: 'app-basepiece',
  templateUrl: './basepiece.component.html',
  styleUrls: ['./basepiece.component.scss']
})
export class BasepieceComponent implements OnInit {

  items: Array<any> = []

  constructor(config:NgbCarouselConfig) {
    config.interval = 4000;  
    config.wrap = true;  
    config.keyboard = false;  
    config.pauseOnHover = false;

 
  }

  ngOnInit(): void {
    // $(document).ready(function() {
    //   alert('I am Called From jQuery');
    // });
  }
}

