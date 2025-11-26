<template>
  <nav class="navbar navbar-dark bg-dark shadow-sm py-2">
    <div class="container d-flex align-items-center justify-content-between">

      <div class="d-flex align-items-center">
        <RouterLink to="/" class="btn btn-outline-light">
          <i class="bi bi-house bi-gger"></i>
        </RouterLink>

      </div>
      <div class="navbar-title">
        {{ pageTitle }}
      </div>
      <div class="d-flex align-items-center gap-3">

        <template v-if="!auth.isLogged">
          <RouterLink to="/login" class="nav-link text-white">Logowanie</RouterLink>
          <RouterLink to="/register" class="nav-link text-white">Rejestracja</RouterLink>
        </template>

        <template v-else>
          <div class="dropdown">
            <a class="nav-link dropdown-toggle text-white" href="#" role="button" data-bs-toggle="dropdown">
              {{ auth.username }}
            </a>
            <ul class="dropdown-menu dropdown-menu-end">
              <li v-if="auth.isAdminUser">
                <RouterLink class="dropdown-item" to="/admin">Panel administracyjny</RouterLink>
              </li>
              <li>
                <a class="dropdown-item" href="#" @click.prevent="logout">Wyloguj się</a>
              </li>
            </ul>
          </div>
        </template>
        <AccessibilityTools />
      </div>

    </div>
  </nav>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useRoute, RouterLink } from "vue-router";
import { useAuthStore } from "../stores/auth";
import AccessibilityTools from "./AccessibilityTools.vue";

const auth = useAuthStore();
const route = useRoute();

const pageTitle = computed(() => route.meta.title ?? "");

function logout() {
  auth.logout();
}
</script>

<style>
.dropdown .nav-link {
  cursor: pointer;
}
</style>
