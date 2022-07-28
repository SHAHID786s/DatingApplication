import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // to fetch data from API

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//declares all components for our application
@NgModule({
  declarations: [AppComponent],
  //
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, // to fetch data from API
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent], // set of componenets we are bootstrapping
})
export class AppModule {}
