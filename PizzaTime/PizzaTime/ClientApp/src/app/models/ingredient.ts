export class Ingredient {

  Name: string;
  Price: number;
  Selected: boolean;

  constructor(name: string, price?: number, selected?: boolean) {

    this.Name = name;
    this.Price = price ? price : 0;
    this.Selected = selected ? selected: false;

  }

}
