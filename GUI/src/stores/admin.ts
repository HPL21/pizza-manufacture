import { defineStore } from "pinia";
import { ref } from "vue";
import api from "@/api";
import type { Order } from "@/types/order";

export const useAdminStore = defineStore("admin", () => {
    const orders = ref<Order[]>([]);
    const loading = ref(false);

    function formatDate(dt: string | null) {
        if (!dt) return "";
        const d = new Date(dt);
        const pad = (n: number) => n.toString().padStart(2, "0");
        return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}`;
    }

    async function fetchOrders() {
        loading.value = true;
        try {
            const res = await api.get("/api/Order");

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
            }));
        } catch (err) {
            console.error("Failed to fetch orders:", err);
        } finally {
            loading.value = false;
        }
    }

    return {
        orders,
        loading,
        fetchOrders
    };
});
