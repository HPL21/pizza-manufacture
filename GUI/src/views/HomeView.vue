<template>
    <div class="bg-pizza">
        <div class="container-sm">
            <div class="row justify-content-center align-items-center">

                <div
                    class="col-lg-8 bg-dark p-5 rounded dynamic-shadow bottom-slide d-flex flex-column justify-content-center align-items-center mt-5">

                    <img src="/images/logo_white.png" alt="logo" width="150" height="150" />

                    <div class="text-center mt-5">
                        <h1 class="font-weight-bold text-light" style="font-size: 3em; margin-bottom: 1em;">
                            Manufaktura pizzy
                        </h1>

                        <h2 class="font-weight-bold text-light" style="font-size: 2em; margin-bottom: 1em;">
                            Wszystko, czego potrzebujesz, w jednym miejscu
                        </h2>
                    </div>

                    <div v-if="auth.isLogged" class="mt-5 text-center">
                        <RouterLink to="/order" class="btn btn-lg blue-background">
                            Złóż zamówienie
                        </RouterLink>
                    </div>

                    <div v-else class="mt-5 text-center">
                        <RouterLink to="/login" class="btn btn-lg blue-background">
                            Rozpocznij
                        </RouterLink>
                    </div>

                </div>

            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { RouterLink } from "vue-router";
import { useAuthStore } from "../stores/auth";

const auth = useAuthStore();
</script>
