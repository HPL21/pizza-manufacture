<template>
  <div class="bg-simple">
    <div class="container-sm min-vh-100 d-flex justify-content-center align-items-center">
      <div class="col-lg-6 bg-dark p-5 mt-5 rounded dynamic-shadow bottom-slide">

        <h1 class="text-center text-light mb-4">Podsumowanie</h1>

        <form @submit.prevent="submitOrder">
          <div v-for="item in menuStore.lastCheckout.pizzas" :key="item.id" class="mb-4 order-item d-flex align-items-center">
            <div class="col-lg-8">
              <p class="text-light"><strong>Nazwa pizzy:</strong> {{ item.name }}</p>
              <p class="text-light"><strong>Ilość:</strong> {{ item.quantity }}</p>
              <p class="text-light"><strong>Cena (1 szt):</strong> {{ item.price }} zł</p>
              <p class="text-light"><strong>Waga (1 szt):</strong> {{ item.weight }} g</p>
              <p class="text-light"><strong>Kalorie (1 szt):</strong> {{ item.calories }} kcal</p>

              <div v-if="item.ingredients.length > 0" class="mt-2">
                <p class="text-light"><strong>Składniki:</strong></p>
                <ul class="text-light">
                  <li v-for="ing in item.ingredients" :key="ing.ingredientId">
                    {{ ing.ingredientName }}: {{ ing.ingredientAmount }} g
                  </li>
                </ul>
              </div>
            </div>
          </div>

          <div class="mb-4 border-bottom">
            <p class="text-light fs-2"><strong>Zamówienie:</strong></p>
            <p class="text-light fs-4"><strong>Kwota zamówienia:</strong> {{ menuStore.lastCheckout.totalPrice }} zł</p>
            <p class="text-light fs-4"><strong>Waga:</strong> {{ menuStore.lastCheckout.totalWeight }} g</p>
            <p class="text-light fs-4"><strong>Kalorie:</strong> {{ menuStore.lastCheckout.totalCalories }} kcal</p>
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
              <input id="card" type="radio" value="card" v-model="form.payment" />
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
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useNotificationStore } from "../stores/notification";
import { useMenuStore } from "../stores/menu";

const router = useRouter();
const notif = useNotificationStore();
const menuStore = useMenuStore();

const form = ref({
  name: "",
  address: "",
  phone: "",
  email: "",
  payment: "cash"
});

onMounted(async () => {
  await menuStore.checkoutCart();
});

const submitOrder = async () => {
  try {
    await menuStore.submitOrder(form.value);
    router.push("/");
    notif.show("Zamówienie zostało złożone!");
  } catch (err) {
    console.error(err);
    notif.show("Wystąpił błąd podczas składania zamówienia.");
  }
};
</script>
