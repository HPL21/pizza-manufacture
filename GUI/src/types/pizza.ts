export interface Ingredient {
  ingredientId: number;
  ingredientName: string;
  ingredientAmount: number;
  price: number;
  weight: number;
  calories: number;
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
  ingredients: Ingredient[];
}

export interface CartItemStored {
  id: number;
  quantity: number;
}
