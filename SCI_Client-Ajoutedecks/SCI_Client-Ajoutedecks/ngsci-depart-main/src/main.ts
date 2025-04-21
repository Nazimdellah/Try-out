import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';


import { Component, importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { withInterceptorsFromDi, provideHttpClient, withInterceptors, HTTP_INTERCEPTORS } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app/app-routing.module';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ApiInterceptor } from './app/services/api.interceptor';


bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(BrowserModule, AppRoutingModule),
        provideAnimations(),
        provideHttpClient(withInterceptorsFromDi(),),{provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true},provideAnimationsAsync(), provideAnimationsAsync()
    ]
})
  .catch(err => console.error(err));


