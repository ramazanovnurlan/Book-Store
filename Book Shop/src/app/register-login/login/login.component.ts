import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../services/auth.service';
import { LanguageService } from '../../language-service.service';
import { ProcessService } from '../../services/process.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  logForm: FormGroup;
  hide = true;

  constructor(private form_b: FormBuilder, private translate: TranslateService,public router: Router,public langService : LanguageService,public authService : AuthService, public processService: ProcessService) { 
    if(authService.isLogin()){
      this.router.navigate(["/books"]);
    }
    console.log(translate)
    this.logForm = this.form_b.group({
      password: [''],
      email: ['', [Validators.required, Validators.email]],
      lang: [langService.getLang().toLowerCase(), Validators.required]
    });
  }
  onSubmit(formValue: FormGroup) {
    if (formValue.invalid ) {
      return;
    }
   
    //formValue.get("lang").setValue(this.langService.getLang());
    console.log(formValue.value)
    this.authService.login(formValue.value, this.langService.getLang().toUpperCase()).subscribe(data => {
      console.log(data)
      if (data.resultSituation?.errorCode && data.resultSituation?.errorCode == 1) {
        console.log(data)
        this.processService.messageDialog(data.resultSituation.result, false);
      } else {
        //this.processService.messageDialog(data.resultSituation.result, false);
       //this.authService.createJWT(data.jwt, "home-banking");
        this.authService.createJWT(data.jwt, "home");
        //this.router.navigate(["/books"]);
      }

    });
  }

  ngOnInit(): void {
  }

}
