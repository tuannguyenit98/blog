import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { IndexComponent } from './index/index.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';
import { ContactComponent } from './contact/contact.component';
import { ComponentModule } from 'src/app/shared/component/component.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { PostSearchComponent } from './post-search/post-search.component';


@NgModule({
    declarations: [
        PostDetailComponent,
        IndexComponent,
        CategoryDetailComponent,
        ContactComponent,
        HomeComponent,
        PostSearchComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        ComponentModule,
        SharedModule
    ]
})
export class HomeModule { }
