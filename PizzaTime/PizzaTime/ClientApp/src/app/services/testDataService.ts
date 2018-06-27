import { abstractDataService } from "./abstractDataService";
import { Pizza } from "../models/pizza";
import { Order } from "../models/order";
import { Ingredient } from "../models/ingredient";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

export class testDataService extends abstractDataService {

    getMenu(): Observable<Pizza[]> {

        let pizzaList = new Array<Pizza>();

        pizzaList.push(new Pizza(
            "Pizza1",
            10,
            "very tasty pizza",
            new Array<Ingredient>(
                new Ingredient("Ingredient1", 1),
                new Ingredient("Ingredient2", 10))
        ));

        pizzaList.push(new Pizza(
            "Pizza2",
            20,
            "",
            new Array<Ingredient>(
                new Ingredient("Ingredient1", 1),
                new Ingredient("Ingredient3"),
                new Ingredient("Ingredient4"),
                new Ingredient("Ingredient5"))
        ));

        return new BehaviorSubject(pizzaList);
    }
    
    saveOrder(order: Order):Observable<boolean> {
        return new BehaviorSubject(true);
    }
}