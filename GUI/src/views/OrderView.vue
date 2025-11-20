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
                                    <td>{{ pizza.ingredients.map(i => i.ingredientName).join(", ") }}</td>
                                    <td>{{ pizza.price }} zł</td>
                                    <td>
                                        <td class="d-flex align-items-center gap-2">

                                          <button
                                            class="btn btn-danger btn-sm"
                                            @click="menu.decreaseQuantity(pizza.id)"
                                          >−</button>

                                          <span style="min-width: 20px; text-align:center;">
                                            {{ menu.quantities[pizza.id] }}
                                          </span>
                                      
                                          <button
                                            class="btn btn-success btn-sm"
                                            @click="menu.increaseQuantity(pizza.id)"
                                          >+</button>
                                      
                                        </td>

                                    </td>
                                </tr>
                            </tbody>
                        </table>

                <div class="mt-5 text-center">
                    <RouterLink
                      to="/order-summary"
                      class="btn btn-danger btn-lg"
                      @click="menu.saveToLocalStorage()"
                    >
                      Podsumowanie
                    </RouterLink>

                </div>
            </div>
        </div>
    </div>
</template>


<script setup lang="ts">
import { onMounted } from "vue";
import { useMenuStore } from "../stores/menu";

const menu = useMenuStore();

onMounted(async () => {
  await menu.getMenu();
});

</script>