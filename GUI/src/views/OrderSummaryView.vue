<template>
  <div class="bg-simple">
    <div class="container-sm min-vh-100 d-flex justify-content-center align-items-center">
      <div class="col-lg-6 bg-dark p-5 mt-5 rounded dynamic-shadow bottom-slide">

        <h1 class="text-center text-light mb-4">Podsumowanie</h1>

        <form id="order-summary-form" @submit="submitOrder">

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
            <input type="text" class="form-control my-4 p-4" name="name" placeholder="Imię i nazwisko" required />
            <input type="text" class="form-control my-4 p-4" name="address" placeholder="Adres" required />
            <input type="text" class="form-control my-4 p-4" name="phone" placeholder="Numer telefonu" required />
            <input type="text" class="form-control my-4 p-4" name="email" placeholder="Adres e-mail" required />
          </div>

          <div class="mb-4">
            <p class="text-light fs-5"><strong>Płatność:</strong></p>
            <div class="d-flex">
              <input type="radio" id="cash" name="payment" value="cash">
              <label for="cash" class="label-custom">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor"
                  class="bi bi-cash-coin" viewBox="0 0 16 16">
                  <path fill-rule="evenodd" d="M11 15a4 4 0 1 0 0-8 4 4 0 0 0 0 8m5-4a5 5 0 1 1-10 0 5 5 0 0 1 10 0" />
                  <path
                    d="M9.438 11.944c.047.596.518 1.06 1.363 1.116v.44h.375v-.443c.875-.061 1.386-.529 1.386-1.207 0-.618-.39-.936-1.09-1.1l-.296-.07v-1.2c.376.043.614.248.671.532h.658c-.047-.575-.54-1.024-1.329-1.073V8.5h-.375v.45c-.747.073-1.255.522-1.255 1.158 0 .562.378.92 1.007 1.066l.248.061v1.272c-.384-.058-.639-.27-.696-.563h-.668zm1.36-1.354c-.369-.085-.569-.26-.569-.522 0-.294.216-.514.572-.578v1.1zm.432.746c.449.104.655.272.655.569 0 .339-.257.571-.709.614v-1.195z" />
                  <path
                    d="M1 0a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h4.083q.088-.517.258-1H3a2 2 0 0 0-2-2V3a2 2 0 0 0 2-2h10a2 2 0 0 0 2 2v3.528c.38.34.717.728 1 1.154V1a1 1 0 0 0-1-1z" />
                  <path d="M9.998 5.083 10 5a2 2 0 1 0-3.132 1.65 6 6 0 0 1 3.13-1.567" />
                </svg>
                <span>Gotówka</span>
              </label>
              <input type="radio" id="card" name="payment" value="card">
              <label for="card" class="label-custom">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor"
                  class="bi bi-credit-card" viewBox="0 0 16 16">
                  <path
                    d="M0 4a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2zm2-1a1 1 0 0 0-1 1v1h14V4a1 1 0 0 0-1-1zm13 4H1v5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1z" />
                  <path d="M2 10a1 1 0 0 1 1-1h1a1 1 0 0 1 1 1v1a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1z" />
                </svg>
                <span>Karta</span>
              </label>
            </div>
          </div>

          <div class="mb-4 d-flex justify-content-between">
            <RouterLink to="/order" class="btn btn-secondary">Powrót</RouterLink>
            <input type="submit" class="btn btn-primary btn-custom" value="Złóż zamówienie">
          </div>

        </form>

      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import api from "../api";
import type { CartItemStored, FullPizza } from "@/types/pizza"


const cart = ref<CartItemStored[]>([]);

const fullCart = ref<FullPizza[]>([]);

onMounted(async () => {
  const stored = localStorage.getItem("cart");
  cart.value = stored ? JSON.parse(stored) : [];

  for (const item of cart.value) {
    const res = await api.get(`/api/Pizza/${item.id}`);

    fullCart.value.push({
      ...res.data,
      quantity: item.quantity
    });
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

const submitOrder = async (e: Event) => {
  e.preventDefault();

  const form = e.target as HTMLFormElement;

  const recipientName = (form.elements.namedItem("name") as HTMLInputElement).value;
  const recipientAddress = (form.elements.namedItem("address") as HTMLInputElement).value;
  const recipientPhone = (form.elements.namedItem("phone") as HTMLInputElement).value;
  const recipientEmail = (form.elements.namedItem("email") as HTMLInputElement).value;

  const paymentRadio = form.elements.namedItem("payment") as RadioNodeList;
  const paymentValue = paymentRadio.value;

  const paymentMethod = paymentValue === "cash" ? 0 : 1;

  const orderItems = fullCart.value.map(p => ({
    pizzaId: p.id,
    itemAmount: p.quantity
  }));

  const order = {
    recipientName,
    recipientAddress,
    recipientPhone,
    recipientEmail,
    paymentMethod,
    orderItems
  };

  try {
    await api.post("/api/Order", order);

    alert("Zamówienie zostało złożone!");

    localStorage.removeItem("cart");

    window.location.href = "/";
  } catch (err) {
    console.error(err);
    alert("Wystąpił błąd podczas składania zamówienia.");
  }
};

</script>
