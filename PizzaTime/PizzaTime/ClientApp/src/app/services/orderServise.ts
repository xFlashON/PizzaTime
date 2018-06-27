import { Order } from "../models/order";
import { Pizza } from "../models/pizza";
import { Injectable } from '@angular/core';
import { Ingredient } from "../models/ingredient";

@Injectable()
export class OrderServise {

    private order: Order;

    constructor() {
        this.order = new Order();
    }

    GetOrder(): Order {
        return this.order;
    }

    AddToOrder(pizza: Pizza) {

        let copy = (JSON.parse(JSON.stringify(pizza)))

        this.order.PizzaList.push(copy);
        this.order.RecalculateTotal();

    }

    RemoveFromOrder(pizza: Pizza) {

        let index = this.order.PizzaList.indexOf(pizza, 0);

        if (index > -1)
            this.order.PizzaList.splice(index, 1);

        this.order.RecalculateTotal();
    }

    ClearOrder(){
        this.order.PizzaList.length=0;
        this.order.RecalculateTotal();
    }

}