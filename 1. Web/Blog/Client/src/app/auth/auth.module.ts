import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SharedModule } from '../shared/shared.module';
import { AuthComponent } from './auth.component';
import { NzFormModule } from 'ng-zorro-antd/form';

@NgModule({
  declarations: [
    SignInComponent,
    SignUpComponent,
    AuthComponent
  ],
  imports: [
    AuthRoutingModule,
    SharedModule,
    NzFormModule
  ]
})
export class AuthModule { }
