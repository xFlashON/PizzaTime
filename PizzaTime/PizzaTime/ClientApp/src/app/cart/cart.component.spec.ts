import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CartComponent } from './cart.component';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../sharedModule';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from '../app-routing.module';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { AppComponent } from '../app.component';
import { PizzaComponent } from '../pizza/pizza.component';
import { LoginComponent } from '../login/login.component';
import { MenuComponent } from '../menu/menu.component';
import { PizzaCaruselComponent } from '../pizza-carusel/pizza-carusel.component';
import { PizzaIngredientsComponent } from '../pizza-ingredients/pizza-ingredients.component';
import { MainComponent } from '../main/main.component';
import { AboutComponent } from '../about/about.component';
import { NotFoundComponent } from '../not-found/not-found.component';
import { RegistrationComponent } from '../registration/registration.component';
import { APP_BASE_HREF } from '@angular/common';
import { abstractDataService } from '../services/abstractDataService';
import { OrderServise } from '../services/orderServise';
import { ProtectionServise } from '../services/protectionServise';
import { testDataService } from '../services/testDataService';
import { HttpClientModule } from '@angular/common/http';
import { ToastyModule } from 'ng2-toasty';
import { AlertService } from '../services/alert.service';

describe('CartComponent', () => {
  let component: CartComponent;
  let fixture: ComponentFixture<CartComponent>;

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
        CarouselModule.forRoot(),
        ToastyModule.forRoot(),
        HttpClientModule
      ],
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
      providers:[OrderServise,
        ProtectionServise,
        AlertService,
        {provide: APP_BASE_HREF, useValue : '/' },
      { provide: 'ApiUrl', useValue: "http://localhost:8080/api" },
      {
        provide: abstractDataService, deps: [OrderServise, ProtectionServise, 'ApiUrl'], useFactory:
          (protectionServise: ProtectionServise) => {
            return new testDataService(protectionServise, 'ApiUrl');
          }
      }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
