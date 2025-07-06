import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { UserComponent } from './user/user.component';
import { TagComponent } from './tag/tag.component';
import { CommentComponent } from './comment/comment.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AdminComponent } from './admin.component';
import { ComponentModule } from "../../shared/component/component.module";
import { TemplateModule } from 'src/app/shared/template/template.module';

@NgModule({
  declarations: [
    UserComponent,
    TagComponent,
    CommentComponent,
    AdminComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    // ComponentModule,
    TemplateModule
]
})
export class AdminModule { }
