import { NgModule,APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuComponent } from './menu/menu.component';
import { FooterComponent } from './footer/footer.component';
import { from } from 'rxjs';
import { MaterialModule } from './modules/material.module';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { BooksComponent } from './books/books.component';
import { BlogComponent } from './blog/blog.component';
import { BasepieceComponent } from './basepiece/basepiece.component';
import { BookCorouselComponent } from './book-corousel/book-corousel.component';
import {  NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './register-login/login/login.component';
import { RegisterComponent } from './register-login/register/register.component';

//import {CabinetModule} from './cabinet/cabinet.module';
//import { MatIconModule } from '@angular/material/icon';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppConfig } from './app.config';
import { HttpClientModule,HttpClient,HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { ProcessService } from './services/process.service';
import { MessageDialog } from './dialog/messageDialog/message-dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { SidenavbaseComponent } from '../app/cabinet/sidenavbase/sidenavbase.component';
import {MatTableModule} from '@angular/material/table';
import {MybooksComponent}  from '../app/cabinet/book/mybooks/mybooks.component';
import {EditBookComponent} from '../app/cabinet/book/edit-book/edit-book.component';
import {NewBokComponent} from './cabinet/book/new-bok/new-bok.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import {AuthorListComponent} from '../app/cabinet/author/author-list/author-list.component';
import {AuthorEditComponent} from '../app/cabinet/author/author-edit/author-edit.component';
import {NewAuthorComponent} from '../app/cabinet/author/new-author/new-author.component';
import {CategoryListComponent} from '../app/cabinet/category/category-list/category-list.component';
import {CategoryEditComponent} from '../app/cabinet/category/category-edit/category-edit.component';
import {NewCategoryComponent} from '../app/cabinet/category/new-category/new-category.component';
import {LanguageListComponent} from '../app/cabinet/booklanguage/language-list/language-list.component';
import {LanguageEditComponent} from '../app/cabinet/booklanguage/language-edit/language-edit.component';
import {NewLanguageComponent} from '../app/cabinet/booklanguage/new-language/new-language.component';



export function initializeApp(appConfig: AppConfig) {
  return () => appConfig.load();
}
export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    FooterComponent,
    HomeComponent,
    AboutComponent,
    BooksComponent,
    BlogComponent,
    BasepieceComponent,
    BookCorouselComponent,
    LoginComponent,
    RegisterComponent,
    MessageDialog,
    SidenavbaseComponent,
    MybooksComponent,
    EditBookComponent,
    NewBokComponent,
    BookDetailComponent,
    AuthorListComponent,
    AuthorEditComponent,
    NewAuthorComponent,
    CategoryListComponent,
    CategoryEditComponent,
    NewCategoryComponent,
    LanguageListComponent,
    LanguageEditComponent,
    NewLanguageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule, 
     NgbModule,
     MaterialModule,
     //CabinetModule,
     HttpClientModule,
     ReactiveFormsModule,
     MatFormFieldModule,
     FormsModule,
     MatTableModule,
	   TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    })  
  ],
  entryComponents: [
    MessageDialog
  ],
  providers: [AuthService, ProcessService, AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    },
    AppConfig, { provide: APP_INITIALIZER, useFactory: initializeApp, deps: [AppConfig, HttpClientModule], multi: true },
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { floatLabel: 'always' } }
  ],
  
  bootstrap: [AppComponent]
})
export class AppModule { }
