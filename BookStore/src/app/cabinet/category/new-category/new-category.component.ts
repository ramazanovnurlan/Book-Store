import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup,Validators } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LanguageService } from '../../../language-service.service';
import { OptionService } from '../../../services/option.service';

@Component({
  selector: 'app-new-category',
  templateUrl: './new-category.component.html',
  styleUrls: ['./new-category.component.scss']
})
export class NewCategoryComponent implements OnInit {

  @ViewChild('fileInput')
  fileInput!: ElementRef;
  fileAttr = 'Choose File';
  id!:number;

  
  newCategoryForm!: FormGroup;
  imgname:any;

  constructor(private form_builder: FormBuilder, public authService: AuthService, public router: Router, public langService: LanguageService, public optionService: OptionService) { 
    this.newCategoryForm = this.form_builder.group({
      title: ['', Validators.required],
    })
  }

  ngOnInit(): void {
  }

  onSubmit(formValue: FormGroup) {
    if (formValue.invalid) {
      console.log("invalid")
      return;
    }
      
    this.optionService.newCategory(formValue.value, this.langService.getLang().toUpperCase()).subscribe(data => {
      console.log("submit")
      if (data.errorCode && data.errorCode == 1) {
        console.log(data)
        this.optionService.messageDialog(data.result, false);
      } else {
        this.optionService.messageDialog(data.result, false);
        this.router.navigate(["/cabinet/category-list"]);
      }

    });
  }

}
