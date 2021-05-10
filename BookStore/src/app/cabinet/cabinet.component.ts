import { Component, OnInit } from '@angular/core';
declare const myalfa:any; //the same call method;
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-cabinet',
  templateUrl: './cabinet.component.html',
  styleUrls: ['./cabinet.component.scss']
})
export class CabinetComponent implements OnInit {

  constructor(public authService:AuthService) { }

  ngOnInit(): void {
  }

  myfunccall(){
    myalfa();
  }
  getUsername() {
    return this.authService.getUserName();
  }

}
