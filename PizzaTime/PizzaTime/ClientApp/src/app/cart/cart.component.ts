import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { OrderServise } from '../services/orderServise';
import { Pizza } from '../models/pizza';
import { ProtectionServise } from '../services/protectionServise';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { abstractDataService } from '../services/abstractDataService';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  order: Order;

  SubmitOrder: FormGroup;

  constructor(private orderServise: OrderServise, private _protectionServise: ProtectionServise, private _dataServise:abstractDataService) {

    this.SubmitOrder = new FormGroup({
      "DeliveryAdress": new FormControl(_protectionServise.user?_protectionServise.user.DefaultDeliveryAdress:'', Validators.required),
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

  SendOrder(){
    if(!this.SubmitOrder.invalid){
      
        let order = this.orderServise.GetOrder();

        order.OrderDate = new Date();

        order.client = this._protectionServise.user;
        order.deliveryAdress = this.SubmitOrder.controls["DeliveryAdress"].value;
        order.comment = this.SubmitOrder.controls["Comment"].value;

        this._dataServise.saveOrder(order).subscribe(res=>{
          if(res==true)
          this.orderServise.ClearOrder();
        
        });

    }
  }

}
