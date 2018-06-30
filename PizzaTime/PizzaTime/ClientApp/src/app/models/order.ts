import { Pizza } from "./pizza";
import { User } from "./user";

export class Order {

  OrderDate: Date;
  Customer: User;
  DeliveryAdress: string;
  Comment: string;
  PizzaList: Pizza[];
  Total: number;

  constructor(){
    this.PizzaList = new Array<Pizza>();
  }

  AddPizza(pizza: Pizza) { }

  RemovePizza(pizza: Pizza) { }

  RecalculateTotal() { 

    let total = 0;

    this.PizzaList.forEach(piza =>{

        total+=piza.Total;

    });

    this.Total = total;

  }

}
