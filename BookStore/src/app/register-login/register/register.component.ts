import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { AuthGuardService } from '../../services/auth-guard.service';
import { AuthService } from '../../services/auth.service';
import { LanguageService } from '../../language-service.service';
import { ProcessService } from '../../services/process.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent?.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
  }
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  regForm: FormGroup;
  hide = true;
  hide2 = true;
  language = ["English", "Azərbaycan", "Русский"];
  matcher = new MyErrorStateMatcher();
  constructor(private form_builder: FormBuilder, public router: Router, public langService: LanguageService, public authService: AuthService, public processService: ProcessService) {
    if(authService.isLogin()){
      this.router.navigate(["/home-banking"]);
    }
    this.regForm = this.form_builder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      username:['', Validators.required],
      birthDate: ['', Validators.required],
      lang: [langService.getLang().toLowerCase(), Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    }, { validator: this.checkPasswords })
  }

  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPassword.value;

    return pass === confirmPass ? null : { notSame: true }
  }

  onSubmit(formValue: FormGroup) {
    if (formValue.invalid) {
      return;
    }
    console.log(formValue.value)

    this.authService.register(formValue.value, this.langService.getLang().toUpperCase()).subscribe(data => {
      console.log(data)
      if (data.errorCode && data.errorCode == 1) {
        console.log(data)
        this.processService.messageDialog(data.result, false);
      } else {
        this.processService.messageDialog(data.result, false);
        this.router.navigate(["/login"]);
      }

    });
  }

  ngOnInit(): void {
  }


}
