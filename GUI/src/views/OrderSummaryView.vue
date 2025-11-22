<template>
  <div class="bg-simple">
    <div class="container-sm min-vh-100 d-flex justify-content-center align-items-center">
      <div class="col-lg-6 bg-dark p-5 mt-5 rounded dynamic-shadow bottom-slide">

        <h1 class="text-center text-light mb-4">Podsumowanie</h1>

        <form @submit.prevent="submitOrder">
          <div v-for="item in fullCart" :key="item.id" class="mb-4 order-item d-flex align-items-center">
            <div class="col-lg-8">
              <p class="text-light"><strong>Nazwa pizzy:</strong> {{ item.name }}</p>
              <p class="text-light"><strong>Ilość:</strong> {{ item.quantity }}</p>
              <p class="text-light"><strong>Cena (1 szt):</strong> {{ item.price }} zł</p>
              <p class="text-light"><strong>Waga (1 szt):</strong> {{ item.weight }} g</p>
              <p class="text-light"><strong>Kalorie (1 szt):</strong> {{ item.calories }} kcal</p>
            </div>
          </div>

          <div class="mb-4 border-bottom">
            <p class="text-light fs-2"><strong>Zamówienie:</strong></p>
            <p class="text-light fs-4"><strong>Kwota zamówienia:</strong> {{ totalPrice }} zł</p>
            <p class="text-light fs-4"><strong>Waga:</strong> {{ totalWeight }} g</p>
            <p class="text-light fs-4"><strong>Kalorie:</strong> {{ totalCalories }} kcal</p>
          </div>

          <div class="mb-4 border-bottom">
            <p class="text-light fs-5"><strong>Dane dostawy:</strong></p>

            <input name="name" v-model="form.name" type="text" class="form-control my-4 p-4" placeholder="Imię i nazwisko" required />

            <input name="address" v-model="form.address" type="text" class="form-control my-4 p-4" placeholder="Adres" required />

            <input name="phone" v-model="form.phone" type="text" class="form-control my-4 p-4" placeholder="Numer telefonu" required />

            <input name="email" v-model="form.email" type="email" class="form-control my-4 p-4" placeholder="Adres e-mail" required />
          </div>

          <div class="mb-4">
            <p class="text-light fs-5"><strong>Płatność:</strong></p>

            <div class="d-flex">
              <input id="cash" type="radio" value="cash" v-model="form.payment" />
              <label for="cash" class="label-custom">
                <i class="bi bi-cash-coin"></i>
                <span>Gotówka</span>
              </label>
              <input  id="card" type="radio" value="card" v-model="form.payment" />
              <label for="card" class="label-custom">
                
                <i class="bi bi-credit-card"></i>
                <span>Karta</span>
              </label>

            </div>
          </div>

          <div class="mb-4 d-flex justify-content-between">
            <RouterLink to="/order" class="btn btn-secondary">Powrót</RouterLink>
            <button type="submit" class="btn btn-primary btn-custom">
              Złóż zamówienie
            </button>
          </div>

        </form>

      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import api from "../api";
import { useRouter } from "vue-router";
import { useNotificationStore } from "../stores/notification";
import type { CartItemStored, FullPizza } from "@/types/pizza";

const router = useRouter();
const notif = useNotificationStore();

const cart = ref<CartItemStored[]>([]);
const fullCart = ref<FullPizza[]>([]);

const form = ref({
  name: "",
  address: "",
  phone: "",
  email: "",
  payment: "cash"
});

onMounted(async () => {
  const stored = localStorage.getItem("cart");
  cart.value = stored ? JSON.parse(stored) : [];

  for (const item of cart.value) {
    const res = await api.get(`/api/Pizza/${item.id}`);
    fullCart.value.push({ ...res.data, quantity: item.quantity });
  }
});

const totalPrice = computed(() =>
  fullCart.value.reduce((sum, p) => sum + p.price * p.quantity, 0)
);

const totalWeight = computed(() =>
  fullCart.value.reduce((sum, p) => sum + (p.weight ?? 0) * p.quantity, 0)
);

const totalCalories = computed(() =>
  fullCart.value.reduce((sum, p) => sum + (p.calories ?? 0) * p.quantity, 0)
);

const submitOrder = async () => {
  const orderItems = fullCart.value.map(p => ({
    pizzaId: p.id,
    itemAmount: p.quantity
  }));

  const order = {
    recipientName: form.value.name,
    recipientAddress: form.value.address,
    recipientPhone: form.value.phone,
    recipientEmail: form.value.email,
    paymentMethod: form.value.payment === "cash" ? 0 : 1,
    orderItems
  };

  try {
    await api.post("/api/Order", order);
    localStorage.removeItem("cart");
    router.push("/");
    notif.show("Zamówienie zostało złożone!");
  } catch (err) {
    console.error(err);
    notif.show("Wystąpił błąd podczas składania zamówienia.");
  }
};
</script>
