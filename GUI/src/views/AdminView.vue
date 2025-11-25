<template>
    <div class="bg-simple">
        <div class="container max-w-90 min-vh-100">
            <h1 class="font-semibold text-light text-3xl mb-4 bg-custom-dark p-4 dynamic-shadow rounded-3">
                Manufaktura pizzy - zamówienia
            </h1>

            <div class="flex justify-end mb-4 gap-3">
                <button class="px-4 py-2 mx-1 rounded bg-light text-dark dynamic-shadow" @click="goToIngredients">
                    ➕ Dodaj składniki
                </button>
                <button class="px-4 py-2 mx-1 rounded bg-light text-dark dynamic-shadow" @click="goToPizza">
                    🍕 Dodaj pizzę
                </button>
            </div>

            <div class="table-responsive dynamic-shadow rounded-3 bg-light p-2">
                <table ref="tableRef" class="display table table-striped" style="width:100%"></table>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch, nextTick } from "vue";
import { useAdminStore } from "@/stores/admin";
import type { Order } from "@/types/order";
import { useRouter } from "vue-router";

import $ from "jquery";
import "datatables.net";

const admin = useAdminStore();
const router = useRouter();

const tableRef = ref<HTMLTableElement | null>(null);
let dataTable: any = null;

const columns = [
    { title: "ID", data: "id" },
    { title: "Data złożenia", data: "placed_at" },
    { title: "Data realizacji", data: "completed_at" },
    { title: "Cena [zł]", data: "total_price" },
    { title: "Waga [g]", data: "total_weight" },
    { title: "Kalorie [kcal]", data: "total_calories" },
    { title: "Adres odbiorcy", data: "recipient_address" },
    { title: "Nr telefonu", data: "recipient_phone" },
    { title: "Email", data: "recipient_email" },
    { title: "Metoda płatności", data: "payment_method" },
    {
        title: "",
        data: null,
        render: (_data: any, _type: any, row: Order) => {
            return `
                <div style="display:flex; justify-content:center; gap:8px;">
                    <button class="edit-btn" data-id="${row.id}">✏️</button>
                    <button class="delete-btn" data-id="${row.id}">🗑️</button>
                    <button class="complete-btn" data-id="${row.id}">✔️</button>
                    <button class="show-btn" data-id="${row.id}">👁️</button>
                </div>
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
        data: admin.orders,
        columns: columns,
        destroy: true,
        searching: false,
        paging: false,
        info: false
    });

    $(tableRef.value).off();

    $(tableRef.value).on("click", ".edit-btn", e => {
        const id = Number($(e.currentTarget).data("id"));
        editOrder(id);
    });

    $(tableRef.value).on("click", ".delete-btn", e => {
        const id = Number($(e.currentTarget).data("id"));
        deleteOrder(id);
    });

    $(tableRef.value).on("click", ".complete-btn", e => {
        const id = Number($(e.currentTarget).data("id"));
        completeOrder(id);
    });

    $(tableRef.value).on("click", ".show-btn", e => {
        const id = Number($(e.currentTarget).data("id"));
        showOrder(id);
    });
}

watch(
    () => admin.orders,
    () => nextTick(() => initTable()),
    { deep: true }
);

onMounted(async () => {
    await admin.fetchOrders();
    initTable();
});

function editOrder(id: number) {
    router.push(`/admin/order/${id}/edit`);
}

async function deleteOrder(id: number) {
    if (confirm("Czy na pewno chcesz usunąć zamówienie?")) {
        // await admin.deleteOrder(id);
        await admin.fetchOrders();
    }
}

async function completeOrder(id: number) {
    if (confirm("Oznaczyć zamówienie jako zrealizowane?")) {
        // await admin.completeOrder(id);
        await admin.fetchOrders();
    }
}

function showOrder(id: number) {
    router.push(`/admin/order/${id}`);
}

function goToIngredients() {
    router.push("/ingredients");
}

function goToPizza() {
    router.push("/pizza");
}
</script>
