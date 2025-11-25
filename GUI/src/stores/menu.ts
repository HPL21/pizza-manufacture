import { defineStore } from "pinia";
import api from "../api";
import type { Pizza, FullPizza } from "@/types/pizza";

export const useMenuStore = defineStore("menu", {
  state: () => ({
    menu: [] as Pizza[],
    quantities: {} as Record<number, number>,
    lastCheckout: {
      totalPrice: 0,
      totalWeight: 0,
      totalCalories: 0,
      pizzas: [] as FullPizza[]
    }
  }),

  getters: {
    isCartEmpty(): boolean {
      return !this.menu.some(pizza => this.quantities[pizza.id]! > 0);
    }
  },

  actions: {
    async getMenu() {
      const res = await api.get<Pizza[]>("/api/Pizza");
      this.menu = res.data;
      this.menu.forEach(p => {
        if (!this.quantities[p.id]) this.quantities[p.id] = 0;
      });
    },
    increaseQuantity(id: number) {
      this.quantities[id] = (this.quantities[id] ?? 0) + 1;
    },
    decreaseQuantity(id: number) {
      if ((this.quantities[id] ?? 0) > 0) this.quantities[id] = this.quantities[id]! - 1;
    },
    saveToLocalStorage() {
      const cart = this.menu
        .filter(p => this.quantities[p.id]! > 0)
        .map(p => ({ id: p.id, quantity: this.quantities[p.id] }));
      localStorage.setItem("cart", JSON.stringify(cart));
    },
    clearCart() {
      Object.keys(this.quantities).forEach(id => {
        this.quantities[Number(id)] = 0;
      });
      localStorage.removeItem("cart");
    },
    async checkoutCart() {
      const cartItems = this.menu
        .filter(p => this.quantities[p.id]! > 0)
        .map(p => ({
          pizzaId: p.id,
          itemAmount: this.quantities[p.id]
        }));
      if (cartItems.length === 0) return;

      const res = await api.post("/api/Pizza/Checkout", { pizzas: cartItems });

      this.lastCheckout.totalPrice = res.data.totalPrice;
      this.lastCheckout.totalWeight = res.data.totalWeight;
      this.lastCheckout.totalCalories = res.data.totalCalories;
      this.lastCheckout.pizzas = res.data.pizzas.map((p: any) => ({
        id: p.id,
        name: p.name,
        weight: p.weight,
        calories: p.calories,
        price: p.price,
        quantity: p.amount,
        ingredients: p.ingredients
      }));
    },
    async submitOrder(form: {
      name: string;
      address: string;
      phone: string;
      email: string;
      payment: string;
    }) {
      if (this.lastCheckout.pizzas.length === 0) throw new Error("Koszyk jest pusty");

      const orderItems = this.lastCheckout.pizzas.map(p => ({
        pizzaId: p.id,
        itemAmount: p.quantity
      }));

      const order = {
        recipientName: form.name,
        recipientAddress: form.address,
        recipientPhone: form.phone,
        recipientEmail: form.email,
        paymentMethod: form.payment === "cash" ? 0 : 1,
        orderItems
      };

      await api.post("/api/Order", order);
      this.clearCart();
    }
  },
});
