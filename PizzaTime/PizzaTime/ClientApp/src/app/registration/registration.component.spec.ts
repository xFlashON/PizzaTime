import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationComponent } from './registration.component';
import { RouterTestingModule } from '@angular/router/testing';
import { AppModule } from '../app.module';
import { CommonModule, APP_BASE_HREF } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../sharedModule';
import { abstractDataService } from '../services/abstractDataService';
import { OrderServise } from '../services/orderServise';
import { ProtectionServise } from '../services/protectionServise';
import { testDataService } from '../services/testDataService';
import { ToastyModule } from 'ng2-toasty';
import { HttpClientModule } from '@angular/common/http';
import { AlertService } from '../services/alert.service';

describe('RegistrationComponent', () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        CommonModule,
        FormsModule,
        SharedModule,
        ReactiveFormsModule,
        ToastyModule.forRoot(),
        HttpClientModule
     ],
     providers:[
      AlertService,
      ProtectionServise, 
      {provide: APP_BASE_HREF, useValue : '/' },
      { provide: 'ApiUrl', useValue: "http://localhost:8080/api" },
      {
        provide: abstractDataService, deps: [OrderServise, ProtectionServise, 'ApiUrl'], useFactory:
          (protectionServise: ProtectionServise) => {
            return new testDataService( protectionServise,'ApiUrl');
          }
      }],
     declarations: [RegistrationComponent]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
