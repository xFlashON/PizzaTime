import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component, Injectable } from '@angular/core';
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
    AppRoutingModule
  ],
  providers: [
    { provide: 'ApiUrl', useValue: "http://localhost:8080/api" },
    {
      provide: abstractDataService, deps: [OrderServise, ProtectionServise, 'ApiUrl'], useFactory:
        (protectionServise: ProtectionServise) => {
          if (environment.production) {
            return new DataService( protectionServise);
          } else {
            return new testDataService( protectionServise);
          }
        }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
