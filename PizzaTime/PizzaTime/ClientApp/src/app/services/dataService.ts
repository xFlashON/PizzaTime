import { Injectable } from "@angular/core";
import {abstractDataService} from "./abstractDataService";
import { Pizza } from "../models/pizza";
import { Order } from "../models/order";
import { Observable } from "rxjs/Observable";

@Injectable()
export class DataService extends abstractDataService {
    
    getMenu(): Observable<Pizza[]> {
        throw new Error("Method not implemented.");
    }
    saveOrder(order: Order):Observable<true> {
        throw new Error("Method not implemented.");
    }
}
