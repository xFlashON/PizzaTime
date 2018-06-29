import { User } from "../models/user";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { Injectable, Inject } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class ProtectionServise {

  private token: string;

  isAuthorised: boolean;

  user: User;

  constructor(private _http: HttpClient, @Inject('ApiUrl') private _apiUrl: string) {
    //cookie?
  }

  Login(login: string, password: string): Observable<boolean> {

    if (login == "" || password == "")
      return new BehaviorSubject(false);

    this.user = new User("UserName", "user@user.com")

    this.isAuthorised = true;

    return new BehaviorSubject(this.isAuthorised);
  }

  LogOff() {

    this.user = null;
    this.isAuthorised = false;
    this.token = null;
  }

  GetToken(): string {
    if (!this.isAuthorised)
      return null;
    return this.token;
  }

  Register(user: User, password: string): Observable<any> {

    let header = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this._http.post(`${this._apiUrl}api/Authorization/Register`, JSON.stringify({
      Name: user.Name,
      Email: user.Email,
      PhoneNumber: user.PhoneNumber,
      DeliveryAdress: user.DefaultDeliveryAdress,
      Password: password
    }), { headers: header });

  }

}
