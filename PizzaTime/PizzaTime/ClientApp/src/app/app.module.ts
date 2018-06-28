import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component, Injectable, Injector } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PizzaComponent } from './pizza/pizza.component';
import { LoginComponent } from './login/login.component';
import { CartComponent } from './cart/cart.component';
import { MenuComponent } from './menu/menu.component';
import { PizzaCaruselComponent } from './pizza-carusel/pizza-carusel.component';
import { PizzaIngredientsComponent } from './pizza-ingredients/pizza-ingredients.component';
import { CommonModule } from '@angular/common';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { abstractDataService } from './services/abstractDataService'
import { DataService } from './services/dataService';
import { environment } from '../environments/environment';
import { testDataService } from './services/testDataService';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { AboutComponent } from './about/about.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { OrderServise } from './services/orderServise';
import { ProtectionServise } from './services/protectionServise';
import { SharedModule } from './sharedModule';
import { RegistrationComponent } from './registration/registration.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    PizzaComponent,
    LoginComponent,
    CartComponent,
    MenuComponent,
    PizzaCaruselComponent,
    PizzaIngredientsComponent,
    MainComponent,
    AboutComponent,
    NotFoundComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule.forRoot(),
    RouterModule,
    CarouselModule.forRoot(),
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    OrderServise,
    ProtectionServise,
    { provide: 'ApiUrl', useFactory: getBaseUrl },
    //{ provide: 'ApiUrl', useValue: "http://localhost:8080/api" },
    {
      provide: abstractDataService, deps: [ProtectionServise, 'ApiUrl', HttpClient], useFactory:
        (protectionServise: ProtectionServise, ApiUrl, HttpClient) => {
          //if (environment.production) {
          return new DataService(protectionServise, ApiUrl, HttpClient);
          //} else {
          //  return new testDataService( protectionServise);
          //}
        }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
