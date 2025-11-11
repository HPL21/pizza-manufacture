<template>
    <div class="bg-pizza">
        <div class="container-sm min-vh-100 d-flex justify-content-center align-items-center">
            <div class="col-lg-6 bg-dark p-5 rounded dynamic-shadow bottom-slide">

                <h1 class="text-center text-light mb-4">Rejestracja</h1>

                <form @submit.prevent="handleRegister">

                    <div class="form-group mb-3">
                        <label class="text-light">Imię i nazwisko</label>
                        <input type="text" v-model="name" class="form-control" required />
                    </div>

                    <div class="form-group mb-3">
                        <label class="text-light">Email</label>
                        <input type="email" v-model="email" class="form-control" required />
                    </div>

                    <div class="form-group mb-3">
                        <label class="text-light">Hasło</label>
                        <input type="password" v-model="password" class="form-control" required />
                    </div>

                    <div class="form-group mb-4">
                        <label class="text-light">Powtórz hasło</label>
                        <input type="password" v-model="passwordConfirm" class="form-control" required />
                    </div>

                    <button type="submit" class="btn btn-danger w-100 btn-lg">
                        Zarejestruj się
                    </button>

                </form>

                <p class="text-center text-light mt-4">
                    Masz już konto?
                    <RouterLink to="/login" class="text-warning">Zaloguj się</RouterLink>
                </p>

            </div>
        </div>
    </div>

</template>

<script setup lang="ts">
import { ref } from "vue";
import { useAuthStore } from "../stores/auth";
import { useRouter } from "vue-router";

const auth = useAuthStore();
const router = useRouter();

const name = ref("");
const email = ref("");
const password = ref("");
const passwordConfirm = ref("");

function handleRegister() {
    if (password.value !== passwordConfirm.value) {
        alert("Hasła nie są takie same!");
        return;
    }

    auth.login();
    router.push("/");
}
</script>
