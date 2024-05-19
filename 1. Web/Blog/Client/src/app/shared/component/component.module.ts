import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SharedModule } from '../shared.module';
import { PaginationComponent } from './pagination/pagination.component';
import { CategoryComponent } from './category/category.component';
import { LoadingComponent } from './loading/loading.component';
import { PostFeaturedComponent } from './post-featured/post-featured.component';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    PaginationComponent,
    CategoryComponent,
    LoadingComponent,
    PostFeaturedComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    PaginationComponent,
    CategoryComponent,
    LoadingComponent,
    PostFeaturedComponent
  ]
})
export class ComponentModule { }
