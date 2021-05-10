import { Component, OnInit } from '@angular/core';

declare const myalfa:any; //the same call method


@Component({
  selector: 'app-sidenavbase',
  templateUrl: './sidenavbase.component.html',
  styleUrls: ['./sidenavbase.component.scss']
})
export class SidenavbaseComponent implements OnInit {

  constructor() { }
  myfunccall(){
    myalfa();
  }

  ngOnInit(): void {
  }

}

