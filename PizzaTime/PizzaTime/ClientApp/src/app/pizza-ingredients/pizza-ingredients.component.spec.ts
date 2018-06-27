import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PizzaIngredientsComponent } from './pizza-ingredients.component';

import { RouterTestingModule } from '@angular/router/testing';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../sharedModule';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from '../app-routing.module';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { AppComponent } from '../app.component';
import { PizzaComponent } from '../pizza/pizza.component';
import { AboutComponent } from '../about/about.component';
import { NotFoundComponent } from '../not-found/not-found.component';
import { RegistrationComponent } from '../registration/registration.component';
import { APP_BASE_HREF } from '@angular/common';
import { abstractDataService } from '../services/abstractDataService';
import { OrderServise } from '../services/orderServise';
import { ProtectionServise } from '../services/protectionServise';
import { testDataService } from '../services/testDataService';
import { LoginComponent } from '../login/login.component';
import { CartComponent } from '../cart/cart.component';
import { MenuComponent } from '../menu/menu.component';
import { MainComponent } from '../main/main.component';
import { PizzaCaruselComponent } from '../pizza-carusel/pizza-carusel.component';

describe('PizzaIngredientsComponent', () => {
  let component: PizzaIngredientsComponent;
  let fixture: ComponentFixture<PizzaIngredientsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule.forRoot(),
        RouterModule,
        AppRoutingModule,
        CarouselModule.forRoot()
      ],
      declarations: [
        AppComponent,
        PizzaComponent,
        LoginComponent,
        CartComponent,
        MenuComponent,
        PizzaIngredientsComponent,
        MainComponent,
        AboutComponent,
        NotFoundComponent,
        RegistrationComponent,
        PizzaCaruselComponent
      ],
      providers:[{provide: APP_BASE_HREF, useValue : '/' },
      { provide: 'ApiUrl', useValue: "http://localhost:8080/api" },
      {
        provide: abstractDataService, deps: [OrderServise, ProtectionServise, 'ApiUrl'], useFactory:
          (protectionServise: ProtectionServise) => {
            return new testDataService( protectionServise);
          }
      }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PizzaIngredientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
