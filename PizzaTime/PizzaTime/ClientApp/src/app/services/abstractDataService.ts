import { Pizza } from "../models/pizza";
import { Order } from "../models/order";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { Inject, Injectable } from '@angular/core';
import { ProtectionServise } from "./protectionServise";

@Injectable()
export abstract class abstractDataService {

  constructor(protected ProtectionServise: ProtectionServise, @Inject('ApiUrl') protected ApiUrl) {
  }

  abstract getMenu(): Observable<Pizza[]>;

  abstract saveOrder(order: Order): Observable<boolean>;

}
