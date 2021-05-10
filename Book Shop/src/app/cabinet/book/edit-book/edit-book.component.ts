import { Component, OnInit,ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LanguageService } from '../../../language-service.service';
import { ProcessService } from '../../../services/process.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { BookPatch } from '../../../models/book-patch.model';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.scss']
})
export class EditBookComponent implements OnInit {
  @ViewChild('fileInput')
  fileInput!: ElementRef;
  fileAttr = 'Choose File';
  language:any;
  imgname:any;
  allowchange:number=0;
  currentbook:any;
  editBookForm!: FormGroup;
  patchList: BookPatch[] = [];
  id!:number;
  authorList!: any[];
  categoryList!: any[];
  languageList!: any[];
  constructor(private form_builder: FormBuilder,public authService: AuthService, public router: Router, public activatedRoute: ActivatedRoute, public langService: LanguageService, public processService: ProcessService) { 
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
    this.selectAuthorTypes();
    this.selectCategoryTypes();
    this.selectLanguageTypes();
  }
  getData(){
    this.processService.getBookDetail(this.id).subscribe(data => {
      
      if (data.errorCode && data.errorCode == 1) {
        
      } else {
        this.selectLanguageTypes();
        this.currentbook=data;
        this.editBookForm = this.form_builder.group({
          title: [data.title, Validators.required],
          description: [data.description, Validators.required],
          price: [data.price, Validators.required],
          imagename: [data.imagename, Validators.required],
          authorkey: [0, Validators.required],
          categorykey: [0, Validators.required],
          languagename: [0, Validators.required]
        })
      }

    });
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
        
            for (let index = 0; index < data.length; index++) {
              if(data[index].id==this.currentbook.languageId){
                 this.language=data[index];
              }
            }
          
      }

    });
  }
  

  onSubmit(formValue:FormGroup) {
    if (formValue.invalid) {
      return;
    }
    console.log(formValue.get("imagename")?.value)
    
    this.patchList.push(<BookPatch>{op: "replace", path: "title", value: formValue.get("title")?.value});
    this.patchList.push(<BookPatch>{op: "replace", path: "description", value: formValue.get("description")?.value});
    this.patchList.push(<BookPatch>{op: "replace", path: "price", value: formValue.get("price")?.value});
    this.patchList.push(<BookPatch>{op: "replace", path: "imagename", value: formValue.get("imagename")?.value});
    this.patchList.push(<BookPatch>{op: "replace", path: "authorkey", value: formValue.get("authorkey")?.value});
    this.patchList.push(<BookPatch>{op: "replace", path: "categorykey", value: formValue.get("categorykey")?.value});
    if(this.allowchange==1){
      this.editBookForm.value.imagename=this.imgname;
      
    }
    
    

    this.processService.editBook(this.editBookForm.value, this.langService.getLang().toUpperCase(), this.id).subscribe(data => {
      console.log(data)
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
    this.allowchange=1;
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
