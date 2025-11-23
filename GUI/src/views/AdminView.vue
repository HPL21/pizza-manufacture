<template>
    <div class="bg-simple">
        <div class="container min-vh-100">
            <h1 class="font-semibold text-light text-3xl mb-4 bg-custom-dark p-4 dynamic-shadow rounded-3">
                Manufaktura pizzy - zamówienia
            </h1>

            <div class="table-responsive dynamic-shadow rounded-3 bg-light p-2">
                <DataTable :data="orders" :columns="columns" class="display" />
            </div>
        </div>
    </div>

</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import api from '../api'

interface Order {
    id: number
    placed_at: string
    completed_at: string
    total_price: number
    total_weight: number
    total_calories: number
    recipient_address: string
    recipient_phone: string
    recipient_email: string
    payment_method: string
}

const orders = ref<Order[]>([])

const columns = [
    { title: 'ID', data: 'id' },
    { title: 'Data złożenia', data: 'placed_at' },
    { title: 'Data realizacji', data: 'completed_at' },
    { title: 'Cena [zł]', data: 'total_price' },
    { title: 'Waga [g]', data: 'total_weight' },
    { title: 'Kalorie [kcal]', data: 'total_calories' },
    { title: 'Adres odbiorcy', data: 'recipient_address' },
    { title: 'Nr telefonu', data: 'recipient_phone' },
    { title: 'Email', data: 'recipient_email' },
    { title: 'Metoda płatności', data: 'payment_method' },
    {
        title: '',
        data: null,
        render: (_data: any, _type: any, row: Order) => {
            return `
        <div style="display:flex; justify-content:center; gap:8px;">
          <button data-id="${row.id}" class="edit-btn">✏️</button>
          <button data-id="${row.id}" class="delete-btn">🗑️</button>
          <button data-id="${row.id}" class="complete-btn">✔️</button>
          <button data-id="${row.id}" class="show-btn">👁️</button>
        </div>`
        }
    }
]

function formatDate(dt: string | null) {
    if (!dt) return ""
    const d = new Date(dt)
    const pad = (n: number) => n.toString().padStart(2, "0")
    return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}`
}


async function loadOrders() {
    try {
        const res = await api.get("/api/Order")
        orders.value = res.data.map((o: any) => ({
            id: o.id,
            placed_at: formatDate(o.placedAt),
            completed_at: formatDate(o.completedAt),
            total_price: o.totalPrice,
            total_weight: o.orderItems.reduce((s: number, x: any) => s + x.weight, 0),
            total_calories: o.orderItems.reduce((s: number, x: any) => s + x.calories, 0),
            recipient_address: o.recipientAddress,
            recipient_phone: o.recipientPhone,
            recipient_email: o.recipientEmail,
            payment_method: o.paymentMethod === 0 ? "Cash" : "Card"
        }))
    } catch (err) {
        console.error("Failed to load orders:", err)
    }
}

onMounted(() => {
    loadOrders()
})


function onEdit(id: number) {
    // TODO
}

function onDelete(id: number) {
    // TODO
}

function onComplete(id: number) {
    // TODO
}

function onShow(id: number) {
    // TODO
}
</script>

<style scoped>
.container {
    max-width: 1200px;
}
</style>
