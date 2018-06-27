import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Pizza } from '../models/pizza';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pizza-ingredients',
  templateUrl: './pizza-ingredients.component.html',
  styleUrls: ['./pizza-ingredients.component.css']
})
export class PizzaIngredientsComponent implements OnInit {

  @Input()
  currentPizza: Pizza;

  @Output()
  selectionChanged = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onSelectionChange(event: any) {
    this.selectionChanged.emit();
  }

}
