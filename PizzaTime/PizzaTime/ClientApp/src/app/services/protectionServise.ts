import { User } from "../models/user";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { Injectable, Inject } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { AlertService, MessageSeverity } from "./alert.service";

@Injectable()
export class ProtectionServise {

  private token: string;

  isAuthorised: boolean;

  user: User;

  constructor(private _http: HttpClient, @Inject('ApiUrl') private _apiUrl: string, private alertService: AlertService) {
  }

  Login(login: string, password: string): Observable<boolean> {

    if (login == "" || password == "")
      return new BehaviorSubject(false);

    let result = new Subject<boolean>();

    let header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });

    let params = new HttpParams()
      .append('username', login)
      .append('password', password);

    this._http.post(`${this._apiUrl}api/Authorization/token`, params.toString(), { headers: header }).subscribe(
      succsess => {
        this.token = succsess['access_token'];
        this.user = new User(succsess['user'], succsess['email'], '', succsess['deliveryAdress'], succsess['id']);
        this.isAuthorised = true;
        result.next(this.isAuthorised);
      },
      error => {
        this.isAuthorised = undefined;
        this.token = undefined;
        this.user = null;
        this.alertService.showMessage(error, '', MessageSeverity.error);
        result.next(this.isAuthorised);
      }
    )

    return result.asObservable();

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
