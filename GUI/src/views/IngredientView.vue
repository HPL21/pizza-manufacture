<template>
    <div class="bg-simple">
        <div class="container max-w-90 min-vh-100">
            <h1 class="font-semibold text-light text-3xl mb-4 bg-custom-dark p-4 dynamic-shadow rounded-3">
                Manufaktura pizzy - składniki
            </h1>

            <div class="flex justify-end mb-4 gap-3">
                <button class="px-4 py-2 rounded bg-light text-dark hover:bg-black" @click="openAddModal">
                    ➕ Nowy składnik
                </button>
            </div>

            <div class="table-responsive dynamic-shadow rounded-3 bg-light p-2">
                <table ref="tableRef" class="display table table-striped" style="width:100%;"></table>
            </div>
        </div>

        <div v-if="showModal" class="modal-overlay">
            <div class="modal-box bg-light p-4 rounded dynamic-shadow">
                <h2 class="text-xl font-bold mb-3">{{ editMode ? "Edytuj składnik" : "Nowy składnik" }}</h2>

                <div class="flex flex-col gap-3">

                    <div class="flex flex-col">
                        <label class="font-semibold mb-1 d-block">Nazwa</label>
                        <input v-model="form.name" class="border p-2 rounded" placeholder="Wpisz nazwę" />
                    </div>

                    <div class="flex flex-col">
                        <label class="font-semibold mb-1 d-block">Cena [zł]</label>
                        <input v-model.number="form.price" type="number" class="border p-2 rounded"
                            placeholder="Cena" />
                    </div>

                    <div class="flex flex-col">
                        <label class="font-semibold mb-1 d-block">Waga [g]</label>
                        <input v-model.number="form.weight" type="number" class="border p-2 rounded"
                            placeholder="Waga" />
                    </div>

                    <div class="flex flex-col">
                        <label class="font-semibold mb-1 d-block">Kalorie [kcal]</label>
                        <input v-model.number="form.calories" type="number" class="border p-2 rounded"
                            placeholder="Kalorie" />
                    </div>

                </div>


                <div class="flex justify-end gap-2 mt-4">
                    <button class="px-3 py-1 bg-gray-400 rounded" @click="closeModal">Anuluj</button>
                    <button class="px-3 py-1 bg-custom-dark text-light rounded" @click="saveIngredient">
                        Zapisz
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch, nextTick } from "vue";
import { useIngredientStore, type Ingredient } from "@/stores/ingredient";
import $ from "jquery";
import "datatables.net";

const store = useIngredientStore();
const tableRef = ref<HTMLTableElement | null>(null);
let dataTable: any = null;

const showModal = ref(false);
const editMode = ref(false);

const form = ref<Ingredient>({
    id: 0,
    name: "",
    price: 0,
    weight: 0,
    calories: 0
});

const columns = [
    { title: "ID", data: "id" },
    { title: "Nazwa", data: "name" },
    { title: "Cena [zł]", data: "price" },
    { title: "Waga [g]", data: "weight" },
    { title: "Kalorie [kcal]", data: "calories" },
    {
        title: "",
        data: null,
        render: (_data: any, _type: any, row: Ingredient) => {
            return `
                <button data-id="${row.id}" class="edit-btn">✏️</button>
                <button data-id="${row.id}" class="delete-btn">🗑️</button>
            `;
        }
    }
];

function initTable() {
    if (!tableRef.value) return;

    if (dataTable) {
        dataTable.destroy();
    }

    dataTable = $(tableRef.value).DataTable({
        data: store.ingredients,
        columns: columns,
        destroy: true,
        searching: false,
        paging: false,
        info: false
    });

    $(tableRef.value).on("click", ".edit-btn", function () {
        const id = Number($(this).data("id"));
        openEditModal(id);
    });

    $(tableRef.value).on("click", ".delete-btn", function () {
        const id = Number($(this).data("id"));
        onDelete(id);
    });
}

watch(
    () => store.ingredients,
    () => {
        nextTick(() => initTable());
    },
    { deep: true }
);

onMounted(async () => {
    await store.fetchIngredients();
    initTable();
});


function openAddModal() {
    editMode.value = false;
    form.value = { id: 0, name: "", price: 0, weight: 0, calories: 0 };
    showModal.value = true;
}

function openEditModal(id: number) {
    const ing = store.ingredients.find(i => i.id === id);
    if (!ing) return;

    editMode.value = true;
    form.value = { ...ing };
    showModal.value = true;
}

function closeModal() {
    showModal.value = false;
}

async function saveIngredient() {
    if (editMode.value) {
        await store.updateIngredient(form.value);
    } else {
        const { id, ...data } = form.value;
        await store.addIngredient(data);
    }

    await store.fetchIngredients();
    closeModal();
}

async function onDelete(id: number) {
    if (confirm("Czy na pewno chcesz usunąć składnik?")) {
        await store.deleteIngredient(id);
        await store.fetchIngredients();
    }
}
</script>

<style>
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
}

.modal-box {
    width: 400px;
}
</style>
