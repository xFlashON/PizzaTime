import { Component, OnInit } from '@angular/core';
import { ProtectionServise } from '../services/protectionServise';
import { OrderServise } from '../services/orderServise';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  isAuthorised:boolean;

  constructor(private _protectionServise:ProtectionServise, private _orderServise:OrderServise, private _router: Router) { }

  ngOnInit() {
  }

  Login(login:string, password:string){

    this._protectionServise.Login(login,password).subscribe(res=>this.isAuthorised=res);

  }

  LogOff(){

    this._protectionServise.LogOff();
    this.isAuthorised = false;

  }

  GetUserString():string{

    if(this._protectionServise.user)
      return this._protectionServise.user.Name;

      return "User is not authorised"
  }

  GetCartStatus():string{

    if(this.isAuthorised)
      {
        return `in cart ${this._orderServise.GetOrder().PizzaList.length} item(s)`;
      }

      return "";
  }

  OpenRegistrationForm (){
    this._router.navigate(["/registration"]);
  }

}
