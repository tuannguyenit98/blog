import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'category'
      },
      {
        path: 'category',
        loadChildren: () => import('./category/category.module').then( m => m.CategoryModule)
      },
      {
        path: 'post',
        loadChildren: () => import('./post/post.module').then( m => m.PostModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
