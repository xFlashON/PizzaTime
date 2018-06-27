import { Ingredient } from "./ingredient";

export class Pizza {

  Name: string;
  Price: number;
  Description: string;
  Total: number;
  ImageUrl: string;

  Ingredients: Ingredient[] = new Array<Ingredient>();

  constructor(name: string, price?: number, description?: string, ingredients?: Ingredient[], imageUrl?: string) {
    this.Name = name;
    this.Price = price ? price : 0;
    this.Ingredients = ingredients ? ingredients : new Array<Ingredient>();
    this.Description = description ? description : "";
    this.ImageUrl = imageUrl ? imageUrl : "";
  }

  RecalculateTotal(): void {

    this.Total = this.Price;

    this.Ingredients.forEach(ingredient => {

      if (ingredient && ingredient.Selected)
        this.Total += ingredient.Price;

    });

  };

}
