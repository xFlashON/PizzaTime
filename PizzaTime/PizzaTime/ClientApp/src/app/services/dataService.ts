import { Injectable, Inject, Injector } from "@angular/core";
import {abstractDataService} from "./abstractDataService";
import { Pizza } from "../models/pizza";
import { Order } from "../models/order";
import { Observable } from "rxjs/Observable";
import { ProtectionServise } from "./protectionServise";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class DataService extends abstractDataService {


  constructor(ProtectionServise: ProtectionServise, @Inject('ApiUrl') ApiUrl, private _http: HttpClient) {

    super(ProtectionServise, ApiUrl);

  }

    getMenu(): Observable<Pizza[]> {

      let header = new HttpHeaders({ 'Content-Type': 'application/json' });

      return this._http.get<Pizza[]>(`${this.ApiUrl}/api/data/GetMenu`, { headers: header });

    }
    saveOrder(order: Order):Observable<true> {
        throw new Error("Method not implemented.");
    }
}
