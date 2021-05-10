import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModuleWithProviders, NgModule} from "@angular/core";
import {CabinetComponent} from './cabinet.component';
//import { BookComponent } from './book/book/book.component';
import { EditBookComponent } from './book/edit-book/edit-book.component';
import { DetailBookComponent } from './book/detail-book/detail-book.component';
import { MybooksComponent } from './book/mybooks/mybooks.component';
import { NewBokComponent } from './book/new-bok/new-bok.component';
import { AuthorListComponent } from './author/author-list/author-list.component';
import { AuthorEditComponent } from './author/author-edit/author-edit.component';
import { CategoryListComponent } from './category/category-list/category-list.component';
import { CategoryEditComponent } from './category/category-edit/category-edit.component';
import { LanguageListComponent } from './booklanguage/language-list/language-list.component';
import { LanguageEditComponent } from './booklanguage/language-edit/language-edit.component';
import { NewAuthorComponent } from './author/new-author/new-author.component';
import { NewCategoryComponent } from './category/new-category/new-category.component';
import { NewLanguageComponent } from './booklanguage/new-language/new-language.component';

@NgModule({
    declarations: [
      CabinetComponent,
      
      EditBookComponent,
      DetailBookComponent,
      MybooksComponent,
      NewBokComponent,
      AuthorListComponent,
      AuthorEditComponent,
      CategoryListComponent,
      CategoryEditComponent,
      LanguageListComponent,
      LanguageEditComponent,
      NewAuthorComponent,
      NewCategoryComponent,
      NewLanguageComponent
  ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule
    ],
    providers: [],
    // bootstrap: [AppComponent]
     bootstrap: []
  })
  export class CabinetModule { 
    static forRoot(): ModuleWithProviders<CabinetModule> {
      return {
          ngModule: CabinetModule,
          providers: []
      };
  }
}

