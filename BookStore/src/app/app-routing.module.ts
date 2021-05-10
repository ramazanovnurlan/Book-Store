import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from './home/home.component';
import {AboutComponent} from './about/about.component';
import {BooksComponent} from './books/books.component';
import {BlogComponent} from './blog/blog.component';
import { LoginComponent } from './register-login/login/login.component';
import { RegisterComponent } from './register-login/register/register.component';
import { CabinetComponent } from './cabinet/cabinet.component';
import { AuthGuardService } from './services/auth-guard.service';
import {SidenavbaseComponent} from '../app/cabinet/sidenavbase/sidenavbase.component';
import {MybooksComponent} from '../app/cabinet/book/mybooks/mybooks.component';
import { EditBookComponent } from './cabinet/book/edit-book/edit-book.component';
import {NewBokComponent} from './cabinet/book/new-bok/new-bok.component';
import {BookDetailComponent} from '../app/book-detail/book-detail.component';
import {AuthorListComponent} from '../app/cabinet/author/author-list/author-list.component';
import {AuthorEditComponent} from '../app/cabinet/author/author-edit/author-edit.component';
import {NewAuthorComponent} from '../app/cabinet/author/new-author/new-author.component';
import {CategoryListComponent} from '../app/cabinet/category/category-list/category-list.component';
import {CategoryEditComponent} from '../app/cabinet/category/category-edit/category-edit.component';
import {NewCategoryComponent} from '../app/cabinet/category/new-category/new-category.component';
import {LanguageListComponent} from '../app/cabinet/booklanguage/language-list/language-list.component';
import {LanguageEditComponent} from '../app/cabinet/booklanguage/language-edit/language-edit.component';
import {NewLanguageComponent} from '../app/cabinet/booklanguage/new-language/new-language.component';

const routes: Routes = [
  {path:'',redirectTo:'/login',pathMatch:'full'},
  {path:'home',component:HomeComponent},
  {path:'about',component:AboutComponent},
  {path:'books',component:BooksComponent},
  {path:'blog',component:BlogComponent},
  {path:'register',component:RegisterComponent},
  {path:'login',component:LoginComponent},
  {
    path:'cabinet',component:SidenavbaseComponent,
    canActivate: [AuthGuardService],
  },
  {
    path:'cabinet/mybooks',component:MybooksComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'cabinet/edit-book/:id',
    canActivate: [AuthGuardService],
    component: EditBookComponent
  },
  {
    path: 'books/:id',
    canActivate: [AuthGuardService],
    component: BookDetailComponent
  },
  {
    path: 'cabinet/new-book',
    canActivate: [AuthGuardService],
    component: NewBokComponent
  },
  {
    path:'cabinet/author-list',component:AuthorListComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'cabinet/author-edit/:id',
    canActivate: [AuthGuardService],
    component: AuthorEditComponent
  },
  {
    path: 'cabinet/new-author',
    canActivate: [AuthGuardService],
    component: NewAuthorComponent
  },
  {
    path:'cabinet/category-list',component:CategoryListComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'cabinet/category-edit/:id',
    canActivate: [AuthGuardService],
    component: CategoryEditComponent
  },
  {
    path: 'cabinet/new-category',
    canActivate: [AuthGuardService],
    component: NewCategoryComponent
  },
  {
    path:'cabinet/language-list',component:LanguageListComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'cabinet/language-edit/:id',
    canActivate: [AuthGuardService],
    component: LanguageEditComponent
  },
  {
    path: 'cabinet/new-language',
    canActivate: [AuthGuardService],
    component: NewLanguageComponent
  }
];
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
