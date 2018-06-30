import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Pizza } from '../models/pizza';
import { Ingredient } from '../models/ingredient';
import { Subject } from 'rxjs/Subject';
import { abstractDataService } from '../services/abstractDataService';
import { OrderServise } from '../services/orderServise';
import { forEach } from '@angular/router/src/utils/collection';
import { AlertService, MessageSeverity } from '../services/alert.service';

@Component({
  selector: 'app-pizza',
  templateUrl: './pizza.component.html',
  styleUrls: ['./pizza.component.css']
})
export class PizzaComponent implements OnInit {

  pizzaList: Pizza[] = new Array<Pizza>();

  currentPizza: Pizza;

  @Input()
  selectedMenuItem: Subject<any>;

  @Output()
  menuLoad = new EventEmitter<Pizza[]>();

  constructor(private dataService: abstractDataService, private orderServise: OrderServise, private alertService: AlertService) {

  }

  ngOnInit() {

    this.dataService.getMenu().subscribe(data => {

      this.pizzaList = data.map(p => new Pizza(p.Name,
        p.Price,
        p.Description,
        p.Ingredients.map(i => new Ingredient(i.Name, i.Price, false, i.Id)),
        p.ImageUrl,
        p.Id));

      if (this.pizzaList.length > 0)
        this.currentPizza = this.pizzaList[0];

      this.calculateTotal();

      this.menuLoad.emit(this.pizzaList);
    });

  }

  currentPizzaChanged(event: number) {

    if (event >= 0 && event <= this.pizzaList.length)
      this.currentPizza = this.pizzaList[event];

    this.calculateTotal();

  }

  calculateTotal() {
    if (this.currentPizza)
      this.currentPizza.RecalculateTotal();
  }

  AddToCart(currentPizza:Pizza){
    this.orderServise.AddToOrder(currentPizza);

    this.alertService.showMessage("Cart", "Pizza successfully added!", MessageSeverity.success);

  }

}
