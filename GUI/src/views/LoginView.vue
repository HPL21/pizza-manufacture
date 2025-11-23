<template>
    <div class="bg-pizza">
        <div class="container-sm min-vh-100 d-flex justify-content-center align-items-center">
            <div class="col-lg-6 bg-dark p-5 rounded dynamic-shadow bottom-slide">

                <h1 class="text-center text-light mb-4">Logowanie</h1>

                <form @submit.prevent="handleLogin">

                    <div class="form-group mb-3">
                        <label class="text-light">Nazwa użytkownika</label>
                        <input type="text" v-model="username" class="form-control" required />
                    </div>

                    <div class="form-group mb-4">
                        <label class="text-light">Hasło</label>
                        <input type="password" v-model="password" class="form-control" required />
                    </div>

                    <button type="submit" class="btn btn-danger w-100 btn-lg">
                        Zaloguj się
                    </button>

                </form>

                <p class="text-center text-light mt-4">
                    Nie masz konta?
                    <RouterLink to="/register" class="text-warning">Zarejestruj się</RouterLink>
                </p>

            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useAuthStore } from "../stores/auth";
import { useNotificationStore } from "../stores/notification";
import { useRouter } from "vue-router";

const auth = useAuthStore();
const notif = useNotificationStore();
const router = useRouter();

const username = ref("");
const password = ref("");

async function handleLogin() {
    try {
        await auth.login(username.value, password.value);
        router.push("/");
        notif.show("Zalogowano pomyślnie!")
    } catch (err: any) {
        notif.show(err.message);
    }
}

</script>
