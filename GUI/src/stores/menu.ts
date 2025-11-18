import { defineStore } from "pinia";
import api from "../api";

export const useMenuStore = defineStore("menu", {
  state: () => ({
    menu: []
  }),

  actions: {
    async getMenu(){
        const res = await api.get("/api/Pizza");
        this.menu = res.data;
        return this.menu;
    },

    addToCart(pizza: any) {
        const cart = JSON.parse(localStorage.getItem("cart") || "[]");
        cart.push(pizza);
        localStorage.setItem("cart", JSON.stringify(cart));
  }
  }
});