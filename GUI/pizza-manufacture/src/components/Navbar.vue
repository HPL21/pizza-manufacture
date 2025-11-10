<template>
  <nav class="navbar navbar-expand-md navbar-dark bg-dark shadow-lg">
    <div class="container">
      <RouterLink class="navbar-brand" to="/">Strona startowa</RouterLink>

      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto"></ul>
        <ul class="navbar-nav ms-auto">
          <template v-if="!auth.isLogged">
            <li class="nav-item">
              <RouterLink class="nav-link" to="/login">Zaloguj się</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link" to="/register">Zarejestruj się</RouterLink>
            </li>
          </template>
          <template v-else>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="navbarDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                {{ auth.username }}
              </a>

              <div class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">

                <RouterLink
                  v-if="auth.isAdminUser"
                  class="dropdown-item"
                  to="/admin"
                >
                  Panel administracyjny
                </RouterLink>

                <a class="dropdown-item" href="#" @click.prevent="logout">
                  Wyloguj się
                </a>
              </div>
            </li>
          </template>

        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { useAuthStore } from "../stores/auth";
import { useRouter, RouterLink } from "vue-router";

const auth = useAuthStore();
const router = useRouter();

function logout() {
  auth.logout();
  router.push("/");
}
</script>
