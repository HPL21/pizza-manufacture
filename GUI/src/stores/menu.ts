import { defineStore } from "pinia";
import api from "../api";

export const useMenuStore = defineStore("menu", {
  state: () => ({
    menu: [],
    quantities: {} as Record<number, number>,
  }),

  actions: {
    async getMenu(){
        const res = await api.get("/api/Pizza");
        this.menu = res.data;
        
        this.menu.forEach(p => {
        if (!this.quantities[p.id]) {
          this.quantities[p.id] = 0;
        }
      });
    },

    increaseQuantity(id: number) {
      this.quantities[id]++;
    },

    decreaseQuantity(id: number) {
      if (this.quantities[id] > 0) this.quantities[id]--;
    },

    saveToLocalStorage() {
      const cart = this.menu
        .filter(p => this.quantities[p.id] > 0)
        .map(p => ({
          ...p,
          quantity: this.quantities[p.id],
        }));

      localStorage.setItem("cart", JSON.stringify(cart));
    }
  }
});