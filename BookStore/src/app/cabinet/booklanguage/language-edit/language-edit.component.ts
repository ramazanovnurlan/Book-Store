import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup,Validators } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router,ActivatedRoute,Params } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LanguageService } from '../../../language-service.service';
import { OptionService } from '../../../services/option.service';
import { BookPatch } from '../../../models/book-patch.model';

@Component({
  selector: 'app-language-edit',
  templateUrl: './language-edit.component.html',
  styleUrls: ['./language-edit.component.scss']
})
export class LanguageEditComponent implements OnInit {

  @ViewChild('fileInput')
  fileInput!: ElementRef;
  fileAttr = 'Choose File';
  language:any;
  imgname:any;
  allowchange:number=0;
  currentlanguage:any;
  editLanguageForm!: FormGroup;
  id!:number;
  patchList: BookPatch[] = [];

  constructor(private form_builder: FormBuilder,public activatedRoute: ActivatedRoute, public authService: AuthService, public router: Router, public langService: LanguageService, public optionService: OptionService) { 
    if(!authService.isLogin()){
      this.router.navigate(["/login"]);
    }
   this.activatedRoute.params.subscribe((params: Params) => {
    this.id = params['id'];
    this.getData();
    
  });
    
  }

  ngOnInit(): void {
  }

  getData(){
    this.optionService.getLanguageDetail(this.id).subscribe(data => {
      if (data.errorCode && data.errorCode == 1) {
        
      } else {
        this.currentlanguage=data;
        this.editLanguageForm = this.form_builder.group({
          title: [data.title, Validators.required],
        })
      }

    });

  }
  onSubmit(formValue:FormGroup) {
    if (formValue.invalid) {
      return;
    }
    
    this.patchList.push(<BookPatch>{op: "replace", path: "tile", value: formValue.get("title")?.value});
    if(this.allowchange==1){
      
    }
    
    

    this.optionService.editLanguage(this.editLanguageForm.value, this.langService.getLang().toUpperCase(), this.id).subscribe(data => {
      console.log(data)
      if (data.errorCode && data.errorCode == 1) {
        console.log(data)
        this.optionService.messageDialog(data.result, false);
      } else {
        this.optionService.messageDialog(data.result, false);
        this.router.navigate(["/cabinet/language-list"]);
      }
    });
  }

}
