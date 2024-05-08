import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { HomeComponent } from './home.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

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
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
