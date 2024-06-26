import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { HomeComponent } from './home.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ContactComponent } from './contact/contact.component';
import { PostSearchComponent } from './post-search/post-search.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: ''
      },
      {
        path: '',
        component: IndexComponent
      },
      {
        path: 'post/:slug',
        component: PostDetailComponent
      },
      {
        path: 'post/:slug/category',
        component: CategoryDetailComponent
      },
      {
        path: 'contact',
        component: ContactComponent
      },
      {
        path: 'search',
        component: PostSearchComponent,
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
