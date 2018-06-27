import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Pizza } from '../models/pizza';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { Subject } from 'rxjs/Subject';

@Component({
  selector: 'app-pizza-carusel',
  templateUrl: './pizza-carusel.component.html',
  styleUrls: ['./pizza-carusel.component.css']
})
export class PizzaCaruselComponent implements OnInit {

  @Input()
  pizzaList: Pizza[];

  @Input()
  selectedMenuItem: Subject<any>;

  @Output()
  currentPizzaChanged = new EventEmitter<number>();

  activeSlideIndex: number = 0;

  constructor() { }

  ngOnInit() {

    if (this.selectedMenuItem)
      this.selectedMenuItem.subscribe(event => {

        if (typeof event === 'number') {
          this.activeSlideIndex = event;
          this.onCurrentPizzaChanged(event);
        }

      });

  }

  onCurrentPizzaChanged(event: number) {
    this.currentPizzaChanged.emit(event);
  }

}
