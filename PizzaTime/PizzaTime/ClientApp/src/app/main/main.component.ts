import { Component, OnInit } from '@angular/core';
import { isDevMode } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Pizza } from '../models/pizza';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})

export class MainComponent implements OnInit {


  pizzaList: Pizza[] = new Array<Pizza>();

  selectedMenuItem: Subject<any> = new Subject();

  ngOnInit() {
  }

  onSelectedMenuItemChanged(selectedItem: number) {

      this.selectedMenuItem.next(selectedItem);

  }

  onMenuLoad(event: any) {

    if (event && event instanceof Array) {
      this.pizzaList.length = 0;
      event.forEach(element => {
        this.pizzaList.push(element);
      });
    }

  }

}
