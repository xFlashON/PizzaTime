import { Pizza } from "../models/pizza";
import { Order } from "../models/order";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { Inject } from '@angular/core';
import { ProtectionServise } from "./protectionServise";

export abstract class abstractDataService{

    @Inject('ApiUrl') protected ApiUrl:string;

    constructor(protected ProtectionServise:ProtectionServise){ 
    }

    abstract getMenu():Observable<Pizza[]>;
    
    abstract saveOrder(order:Order):Observable<boolean>;

}