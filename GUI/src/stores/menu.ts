import { defineStore } from "pinia";
import api from "../api";
import type { Pizza } from "@/types/pizza"

export const useMenuStore = defineStore("menu", {
  state: () => ({
    menu: [] as Pizza[],
    quantities: {} as Record<number, number>,
  }),

  actions: {
    async getMenu() {
      const res = await api.get<Pizza[]>("/api/Pizza");
      this.menu = res.data;
      this.menu.forEach(p => {
        if (!this.quantities[p.id]) {
          this.quantities[p.id] = 0;
        }
      });
    },
    increaseQuantity(id: number) {
      this.quantities[id] = (this.quantities[id] ?? 0) + 1;
    },
    decreaseQuantity(id: number) {
      if ((this.quantities[id] ?? 0) > 0) {
        this.quantities[id] = this.quantities[id]! - 1;
      }
    },
    saveToLocalStorage() {
      const cart = this.menu
        .filter(p => this.quantities[p.id]! > 0)
        .map(p => ({
          id: p.id,
          quantity: this.quantities[p.id],
        }));
      localStorage.setItem("cart", JSON.stringify(cart));
    }
  }
});
