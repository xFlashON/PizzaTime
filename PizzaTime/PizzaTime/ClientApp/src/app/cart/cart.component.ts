import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { OrderServise } from '../services/orderServise';
import { Pizza } from '../models/pizza';
import { ProtectionServise } from '../services/protectionServise';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { abstractDataService } from '../services/abstractDataService';
import { AlertService } from '../services/alert.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  order: Order;

  SubmitOrder: FormGroup;

  constructor(private orderServise: OrderServise, private _protectionServise: ProtectionServise, private _dataServise: abstractDataService, private alertService: AlertService) {

    this.SubmitOrder = new FormGroup({
      "DeliveryAdress": new FormControl(_protectionServise.user ? _protectionServise.user.DefaultDeliveryAdress : '', Validators.required),
      "Comment": new FormControl("")
    });

  }

  ngOnInit() {

    this.order = this.orderServise.GetOrder();

  }

  RemoveItem(pizza: Pizza) {
    this.orderServise.RemoveFromOrder(pizza);
  }

  IsAutorised(): boolean {
    return this._protectionServise.isAuthorised;
  }

  SendOrder() {
    if (!this.SubmitOrder.invalid) {

      let order = this.orderServise.GetOrder();

      order.OrderDate = new Date();

      order.Customer = this._protectionServise.user;
      order.DeliveryAdress = this.SubmitOrder.controls["DeliveryAdress"].value;
      order.Comment = this.SubmitOrder.controls["Comment"].value;

      this._dataServise.saveOrder(order).subscribe(res => {
        if (res == true) {
          this.SubmitOrder.controls["Comment"].reset();
          this.orderServise.ClearOrder();
        }

      });

    }
  }

}
