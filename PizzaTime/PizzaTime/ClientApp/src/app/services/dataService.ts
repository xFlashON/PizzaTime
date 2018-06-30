import { Injectable, Inject, Injector } from "@angular/core";
import { abstractDataService } from "./abstractDataService";
import { Pizza } from "../models/pizza";
import { Order } from "../models/order";
import { Observable } from "rxjs/Observable";
import { ProtectionServise } from "./protectionServise";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Subject } from "rxjs";
import { AlertService, MessageSeverity } from "./alert.service";

@Injectable()
export class DataService extends abstractDataService {


  constructor(ProtectionServise: ProtectionServise, @Inject('ApiUrl') ApiUrl, private _http: HttpClient, private alertService: AlertService) {

    super(ProtectionServise, ApiUrl);

  }

  getMenu(): Observable<Pizza[]> {

    let header = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this._http.get<Pizza[]>(`${this.ApiUrl}/api/data/GetMenu`, { headers: header });

  }
  saveOrder(order: Order): Observable<boolean> {

    var result = new Subject<boolean>();

    if (this.ProtectionServise.isAuthorised) {

      let header = new HttpHeaders({
        'Authorization': 'Bearer ' + this.ProtectionServise.GetToken(),
        'Content-Type': 'application/json'
      });

      this._http.post(`${this.ApiUrl}/api/data/SaveOrder`, order, { headers: header }).subscribe(
        success => {
          result.next(true);
          this.alertService.showMessage("Success", `Created order №${success}`, MessageSeverity.success);
        },
        error => {
          result.next(false);
          this.alertService.showMessage(error, "", MessageSeverity.error);
        }
      );

    }
    else {

    }

    return result.asObservable();

  }
}
