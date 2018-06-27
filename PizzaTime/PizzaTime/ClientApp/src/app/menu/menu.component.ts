import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Pizza } from '../models/pizza';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  @Input()
  pizzaList:Pizza[];

  @Output()
  menuItemSelected = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }

}
