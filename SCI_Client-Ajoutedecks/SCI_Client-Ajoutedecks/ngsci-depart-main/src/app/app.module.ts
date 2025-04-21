import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ApiInterceptor } from './services/api.interceptor';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// TODO: Ajouter FormsModule
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [

    ],
    imports: [
        HttpClientModule,
        AppComponent,
        CommonModule,
        BrowserModule,
        AppRoutingModule,
        FormsModule,
        MatToolbarModule
    ],
    providers: [
    ],
    bootstrap: []

})
export class AppModule { }