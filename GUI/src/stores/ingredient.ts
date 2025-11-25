import { defineStore } from "pinia";
import { ref } from "vue";
import api from "@/api";

export interface Ingredient {
    id: number;
    name: string;
    price: number;
    weight: number;
    calories: number;
}

export const useIngredientStore = defineStore("ingredient", () => {
    const ingredients = ref<Ingredient[]>([]);
    const loading = ref(false);

    async function fetchIngredients() {
        loading.value = true;
        try {
            const res = await api.get("/api/Ingredient");
            ingredients.value = res.data;
        } catch (err) {
            console.error("Fetch ingredients failed:", err);
        } finally {
            loading.value = false;
        }
    }

    async function addIngredient(item: Omit<Ingredient, "id">) {
        try {
            const res = await api.post("/api/Ingredient", item);
            ingredients.value.push(res.data);
        } catch (err) {
            console.error("Add ingredient failed:", err);
        }
    }

    async function updateIngredient(item: Ingredient) {
        try {
            await api.put(`/api/Ingredient/${item.id}`, item);
            const idx = ingredients.value.findIndex(x => x.id === item.id);
            if (idx !== -1) ingredients.value[idx] = item;
        } catch (err) {
            console.error("Update ingredient failed:", err);
        }
    }

    async function deleteIngredient(id: number) {
        try {
            await api.delete(`/api/Ingredient/${id}`);
            ingredients.value = ingredients.value.filter(i => i.id !== id);
        } catch (err) {
            console.error("Delete ingredient failed:", err);
        }
    }

    return {
        ingredients,
        loading,
        fetchIngredients,
        addIngredient,
        updateIngredient,
        deleteIngredient
    };
});
