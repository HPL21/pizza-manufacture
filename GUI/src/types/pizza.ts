export interface PizzaIngredient {
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
  ingredients: PizzaIngredient[];
}

export interface FullPizza {
  id: number;
  name: string;
  price: number;
  weight: number;
  calories: number;
  quantity: number;
  ingredients: PizzaIngredient[];
}

export interface CartItemStored {
  id: number;
  quantity: number;
}
