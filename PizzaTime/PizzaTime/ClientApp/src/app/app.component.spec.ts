import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { SharedModule } from './sharedModule';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { MainComponent } from './main/main.component';
import { CartComponent } from './cart/cart.component';
import { PizzaCaruselComponent } from './pizza-carusel/pizza-carusel.component';
import { PizzaIngredientsComponent } from './pizza-ingredients/pizza-ingredients.component';
import { AboutComponent } from './about/about.component';
import { RegistrationComponent } from './registration/registration.component';
import { PizzaComponent } from './pizza/pizza.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { APP_BASE_HREF } from '@angular/common';
import { AppModule } from './app.module';
import { ToastyModule } from 'ng2-toasty';
import { AlertService } from './services/alert.service';
import { ProtectionServise } from './services/protectionServise';
import { OrderServise } from './services/orderServise';
import { HttpClientModule } from '@angular/common/http';
describe('AppComponent', () => {
  beforeEach(async(() => {

    const appRoutes: Routes = [
      { path: '', component: MainComponent },
      { path: 'cart', component: CartComponent },
      { path: 'about', component: AboutComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: '**', component: NotFoundComponent }
    ]

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
      providers: [
        OrderServise,
        ProtectionServise,
        AlertService,
        { provide: APP_BASE_HREF, useValue: '/' },
        { provide: 'ApiUrl', useValue: '/'}
      ]
    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));

  it('should have login block', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('#loginBlock')).toBeDefined();
  }));


  it('should have cart block', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('#cart')).toBeDefined();
  }));
});
