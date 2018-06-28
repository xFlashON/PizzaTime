export class Ingredient {

  Id: string;
  Name: string;
  Price: number;
  Selected: boolean;

  constructor(name: string, price?: number, selected?: boolean, id?:string) {

    this.Name = name;
    this.Price = price ? price : 0;
    this.Selected = selected ? selected : false;
    this.Id = id ? id : "";

  }

}
