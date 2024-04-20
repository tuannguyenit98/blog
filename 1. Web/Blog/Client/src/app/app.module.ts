import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from  '@angular/common/http';
import { ErrorInterceptor } from './guards/error.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contact/contact.component';
import { ComponentModule } from "./shared/component/component.module";

@NgModule({
    declarations: [
        AppComponent,
        ContactComponent,
    ],
    providers: [
        { provide: 'API_BASE_URL', useFactory: getApiBaseUrl, deps: [] },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    bootstrap: [AppComponent],
    imports: [
        CommonModule,
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        SharedModule,
        BrowserAnimationsModule,
        ComponentModule
    ]
})
export class AppModule { }

export function getApiBaseUrl() {
  return 'http://localhost:55288/';
}
