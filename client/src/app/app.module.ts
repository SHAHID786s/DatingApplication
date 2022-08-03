import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // to fetch data from API

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

//declares all components for our application
@NgModule({
  declarations: [AppComponent, NavComponent, HomeComponent, RegisterComponent],
  //
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, // to fetch data from API
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot(), // for root means it needs to intialise some services
  ],
  providers: [],
  bootstrap: [AppComponent], // set of componenets we are bootstrapping
})
export class AppModule {}
