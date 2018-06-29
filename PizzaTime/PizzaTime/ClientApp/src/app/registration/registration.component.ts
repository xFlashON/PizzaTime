import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../models/user';
import { PasswordValidation } from './password-validation';
import { ProtectionServise } from '../services/protectionServise';
import { AlertService, MessageSeverity } from '../services/alert.service';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  RegistrationForm: FormGroup;
  RegistrationComplete: boolean;

  constructor(fb: FormBuilder, private _protectionServise: ProtectionServise,private alertService:AlertService) {

    this.RegistrationForm = fb.group({
      'Name': ["", [Validators.required, Validators.minLength(3)]],
      'Email': ["", [Validators.required, Validators.pattern("[a-zA-Z_]+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}")]],
      'PhoneNumber': ["", Validators.pattern("^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")],
      'DefaultDeliveryAdress': '',
      'Password': ['', [Validators.required, Validators.minLength(5)]],
      'PasswordConfirm': ''
    },
      {
        validator: PasswordValidation.MatchPassword
      });

  }

  ngOnInit() {

    this.RegistrationComplete = false;

  }

  Submit() {

    if (!this.RegistrationForm.valid)
      return;

    let user = new User(
      this.RegistrationForm.controls.Name.value,
      this.RegistrationForm.controls.Email.value,
      this.RegistrationForm.controls.PhoneNumber.value,
      this.RegistrationForm.controls.DefaultDeliveryAdress.value
    );

    this._protectionServise.Register(user, this.RegistrationForm.controls.Password.value).subscribe(
      res => {

        this.RegistrationComplete = true;

      },
      error => {

        this.alertService.showStickyMessage(error);

        console.log(error);
      });

  }

}
