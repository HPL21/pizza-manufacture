export interface Ingredient {
  ingredientName: string;
}

export interface Pizza {
  id: number;
  name: string;
  price: number;
  ingredients: Ingredient[];
}

export interface FullPizza {
  id: number;
  name: string;
  price: number;
  weight: number;
  calories: number;
  quantity: number;
}

export interface CartItemStored {
  id: number;
  quantity: number;
}
