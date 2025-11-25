<template>
    <div class="bg-simple">
        <div class="container max-w-90 min-vh-100">
            <h1 class="font-semibold text-light text-3xl mb-4 bg-custom-dark p-4 dynamic-shadow rounded-3">
                Manufaktura pizzy - pizze
            </h1>

            <div class="flex justify-end mb-4 gap-3">
                <button class="px-4 py-2 rounded bg-light text-dark dynamic-shadow" @click="openAddModal">
                    ➕ Nowa pizza
                </button>
            </div>

            <div class="table-responsive dynamic-shadow rounded-3 bg-light p-2">
                <table ref="tableRef" class="display table table-striped" style="width:100%"></table>
            </div>
        </div>

        <div v-if="showModal" class="modal-overlay">
            <div class="modal-box bg-light p-4 rounded dynamic-shadow">
                <h2 class="text-xl font-bold mb-3">
                    {{ editMode ? 'Edytuj pizzę' : 'Nowa pizza' }}
                </h2>

                <div class="flex flex-col gap-3">

                    <div class="flex flex-col">
                        <label class="font-semibold mb-1 d-block">Nazwa</label>
                        <input v-model="form.name" class="border p-2 rounded" />
                    </div>

                    <div class="flex flex-col">
                        <label class="font-semibold mb-1 d-block">Cena [zł]</label>
                        <input v-model.number="form.price" type="number" class="border p-2 rounded" />
                    </div>

                    <div>
                        <label class="font-semibold mb-1 d-block">Składniki</label>

                        <div v-for="(item, idx) in form.pizzaIngredients" :key="idx" class="flex gap-2 mb-2">

                            <select v-model="item.ingredientId" class="border p-2 rounded flex-grow">
                                <option disabled value="">-- wybierz składnik --</option>
                                <!-- @vue-ignore -->
                                <option v-for="ing in ingredients" :key="ing.id" :value="ing.id">
                                    <!-- @vue-ignore -->
                                    {{ ing.name }}
                                </option>
                            </select>

                            <input type="number" v-model.number="item.ingredientAmount"
                                   min="1" class="border p-2 rounded w-24" />

                            <button class="px-2 py-1 bg-red-400 rounded" @click="removeIngredient(idx)">🗑️</button>
                        </div>

                        <button class="px-3 py-1 bg-custom-dark text-light rounded" @click="addIngredientRow">
                            ➕ Dodaj składnik
                        </button>
                    </div>
                </div>

                <div class="flex justify-end gap-2 mt-4">
                    <button class="px-3 py-1 bg-gray-400 rounded" @click="closeModal">Anuluj</button>
                    <button class="px-3 py-1 bg-custom-dark text-light rounded" @click="savePizza">Zapisz</button>
                </div>

            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch, nextTick } from "vue";
import { usePizzaStore } from "@/stores/pizza";
import { useIngredientStore, type Ingredient } from "@/stores/ingredient";
import type { Pizza } from "@/types/pizza";

import $ from "jquery";
import "datatables.net";

const store = usePizzaStore();
const ingredientStore = useIngredientStore();

const tableRef = ref<HTMLTableElement | null>(null);
let dataTable: any = null;

const showModal = ref(false);
const editMode = ref(false);

const ingredients = ref<Ingredient>();

const form = ref({
    id: 0,
    name: "",
    price: 0,
    pizzaIngredients: [] as { ingredientId: number; ingredientAmount: number }[]
});


const columns = [
    { title: "ID", data: "id" },
    { title: "Nazwa", data: "name" },
    { title: "Waga [g]", data: "weight" },
    { title: "Kalorie [kcal]", data: "calories" },
    { title: "Cena [zł]", data: "price" },
    {
        title: "Składniki",
        data: "ingredients",
        render: (_data: any, _t: any, row: Pizza) =>
            row.ingredients.map(i => i.ingredientName).join(", ")
    },
    {
        title: "",
        data: null,
        render: (_d: any, _t: any, row: Pizza) =>
            `
            <div style="display:flex; justify-content:center; gap:8px;">
                <button class="edit-btn" data-id="${row.id}">✏️</button>
                <button class="delete-btn" data-id="${row.id}">🗑️</button>
            </div>
            `
    }
];


function initTable() {
    if (!tableRef.value) return;

    if (dataTable) dataTable.destroy();

    dataTable = $(tableRef.value).DataTable({
        data: store.pizzas,
        columns,
        searching: false,
        paging: false,
        info: false,
        destroy: true
    });

    $(tableRef.value).off();

    $(tableRef.value).on("click", ".edit-btn", e => {
        openEditModal(Number($(e.currentTarget).data("id")));
    });

    $(tableRef.value).on("click", ".delete-btn", e => {
        deletePizza(Number($(e.currentTarget).data("id")));
    });
}


watch(
    () => store.pizzas,
    () => nextTick(initTable),
    { deep: true }
);


onMounted(async () => {
    await ingredientStore.fetchIngredients();
    // @ts-ignore
    ingredients.value = ingredientStore.ingredients;

    await store.fetchPizzas();
    initTable();
});

function openAddModal() {
    editMode.value = false;
    form.value = {
        id: 0,
        name: "",
        price: 0,
        pizzaIngredients: []
    };
    showModal.value = true;
}

function openEditModal(id: number) {
    const pizza = store.pizzas.find(x => x.id === id);
    if (!pizza) return;

    editMode.value = true;

    form.value = {
        id: pizza.id,
        name: pizza.name,
        price: pizza.price,
        pizzaIngredients: pizza.ingredients.map(i => ({
            ingredientId: i.ingredientId,
            ingredientAmount: i.ingredientAmount ?? 1
        }))
    };

    showModal.value = true;
}

function addIngredientRow() {
    form.value.pizzaIngredients.push({
        ingredientId: 0,
        ingredientAmount: 1
    });
}

function removeIngredient(idx: number) {
    form.value.pizzaIngredients.splice(idx, 1);
}

function closeModal() {
    showModal.value = false;
}

async function savePizza() {
    const payload: any = {
        name: form.value.name,
        price: form.value.price,
        weight: 1, // TODO: CHANGE AFTER API UPDATE
        calories: 1, // TODO: CHANGE AFTER API UPDATE
        pizzaIngredients: form.value.pizzaIngredients
    };

    if (editMode.value) {
        payload.id = form.value.id;
        await store.updatePizza(payload as any);
    } else {
        await store.addPizza(payload as any);
    }

    await store.fetchPizzas();
    closeModal();
}

async function deletePizza(id: number) {
    if (!confirm("Usunąć pizzę?")) return;

    await store.deletePizza(id);
    await store.fetchPizzas();
}
</script>

<style>
.modal-overlay {
    position: fixed;
    inset: 0;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
}

.modal-box {
    width: 500px;
}
</style>
