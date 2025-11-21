<template>
  <div class="bg-simple">
    <div class="container-sm min-vh-100 d-flex justify-content-center align-items-center">
      <div class="col-lg-6 bg-dark p-5 rounded dynamic-shadow bottom-slide">

        <h1 class="text-center text-light mb-4">Złóż zamówienie</h1>

        <table class="table table-striped table-hover table-light">
          <thead class="thead-dark">
            <tr>
              <th>Nazwa</th>
              <th>Składniki</th>
              <th>Cena</th>
              <th></th>
            </tr>
          </thead>
          <tbody>

            <tr v-for="pizza in menu.menu">
              <td>{{ pizza.name }}</td>
              <td>{{pizza.ingredients.map(i => i.ingredientName).join(", ")}}</td>
              <td>{{ pizza.price }} zł</td>
              <td>
              <td class="d-flex align-items-center gap-2">

                <button class="btn btn-danger btn-sm" @click="menu.decreaseQuantity(pizza.id)">−</button>

                <span style="min-width: 20px; text-align:center;">
                  {{ menu.quantities[pizza.id] }}
                </span>

                <button class="btn btn-success btn-sm" @click="menu.increaseQuantity(pizza.id)">+</button>

              </td>

              </td>
            </tr>
          </tbody>
        </table>

        <div class="mt-5 text-center" @click.prevent="handleSummaryClick">
          <RouterLink to="/order-summary" class="btn btn-danger btn-lg" :class="{ blocked: isCartEmpty }">
            Podsumowanie
          </RouterLink>
        </div>
      </div>
    </div>
  </div>
</template>


<script setup lang="ts">
import { onMounted, computed } from "vue";
import { useMenuStore } from "../stores/menu";
import { useNotificationStore } from "../stores/notification";

const menu = useMenuStore();
const notif = useNotificationStore();

onMounted(async () => {
  await menu.getMenu();
});

const isCartEmpty = computed(() =>
  Object.values(menu.quantities).every(q => q === 0)
);

function handleSummaryClick() {
  if (isCartEmpty.value) {
    notif.show("Twój koszyk jest pusty.");
    return;
  }
  menu.saveToLocalStorage();
}
</script>
<style scoped>
.blocked {
  opacity: 0.5;
  pointer-events: none;
  cursor: not-allowed;
}
</style>