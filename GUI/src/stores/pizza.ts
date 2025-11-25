import { defineStore } from 'pinia';
import type { Pizza } from '@/types/pizza';
import api from "@/api";

export const usePizzaStore = defineStore('pizza', {
    state: () => ({
        pizzas: [] as Pizza[],
    }),

    actions: {
        async fetchPizzas() {
            try {
                const res = await api.get('/api/Pizza');
                this.pizzas = res.data;
            } catch (err) {
                console.error('Błąd pobierania pizz:', err);
            }
        },

        async addPizza(pizza: Pizza) {
            try {
                await api.post('/api/Pizza', pizza);
                await this.fetchPizzas();
            } catch (err) {
                console.error('Błąd dodawania pizzy:', err);
            }
        },

        async updatePizza(pizza: Pizza) {
            try {
                await api.put(`/api/Pizza/${pizza.id}`, pizza);
                await this.fetchPizzas();
            } catch (err) {
                console.error('Błąd edycji pizzy:', err);
            }
        },

        async deletePizza(id: number) {
            try {
                await api.delete(`/api/Pizza/${id}`);
                this.pizzas = this.pizzas.filter(x => x.id !== id);
            } catch (err) {
                console.error('Błąd usuwania pizzy:', err);
            }
        }
    }
});