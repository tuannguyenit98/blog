import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { ComponentModule } from "../shared/component/component.module";
import { PostDetailComponent } from './post-detail/post-detail.component';
import { IndexComponent } from './index/index.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
    declarations: [
        HomeComponent,
        PostDetailComponent,
        IndexComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        ComponentModule,
        SharedModule
    ]
})
export class HomeModule { }
