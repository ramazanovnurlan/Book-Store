import { Component, OnInit,ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LanguageService } from '../../../language-service.service';
import { ProcessService } from '../../../services/process.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { BookPatch } from '../../../models/book-patch.model';

@Component({
  selector: 'app-new-bok',
  templateUrl: './new-bok.component.html',
  styleUrls: ['./new-bok.component.scss']
})
export class NewBokComponent implements OnInit {
  @ViewChild('fileInput')
  fileInput!: ElementRef;
  fileAttr = 'Choose File';
  id!:number;

  authorList!: any[];
  categoryList!: any[];
  languageList!: any[];
  newBookForm!: FormGroup;
  imgname:any;
  constructor(private form_builder: FormBuilder, public router: Router, public langService: LanguageService, public authService: AuthService, public processService: ProcessService) { 
    this.newBookForm = this.form_builder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, Validators.required],
      author: ['', Validators.required],
      imagename:[''],
      category: ['', Validators.required],
      languagename: ['', Validators.required],
    })
  }

  ngOnInit(): void {
    this.selectAuthorTypes();
    this.selectCategoryTypes();
    this.selectLanguageTypes();
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
      }

    });
  }
  selectLanguageTypes(){
    this.processService.getLanguageList(this.langService.getLang().toUpperCase()).subscribe(data => {
      
      if (data.errorCode && data.errorCode == 1) {
        this.processService.messageDialog(data.errorMessage, false);
      } else {
        this.languageList = data; 
        console.log(this.languageList);
      }

    });
  }

  onSubmit(formValue: FormGroup) {
    if (formValue.invalid) {
      console.log("invalid")
      return;
    }
      
    formValue.value.imagename=this.imgname;
    console.log(formValue.value)
    
    this.processService.newBook(formValue.value, this.langService.getLang().toUpperCase()).subscribe(data => {
      console.log("submit")
      if (data.errorCode && data.errorCode == 1) {
        console.log(data)
        this.processService.messageDialog(data.result, false);
      } else {
        this.processService.messageDialog(data.result, false);
        this.router.navigate(["/cabinet/mybooks"]);
      }

    });
  }

  uploadFileEvt(imgFile: any) {
    
    if (imgFile.target.files && imgFile.target.files[0]) {
      this.imgname=imgFile.target.files[0].name;
      this.fileAttr = '';
      Array.from(imgFile.target.files).forEach((file: any) => {
        this.fileAttr += file.name + ' - ';
      });

      // HTML5 FileReader API
      let reader = new FileReader();
      reader.onload = (e: any) => {
        let image = new Image();
        image.src = e.target.result;
        
        image.onload = rs => {
          let imgBase64Path = e.target.result;
        };
      };
      reader.readAsDataURL(imgFile.target.files[0]);
      
      // Reset if duplicate image uploaded again
      this.fileInput.nativeElement.value = "";
    } 
    else {
      this.fileAttr = 'Choose File';
    }
  }

}
