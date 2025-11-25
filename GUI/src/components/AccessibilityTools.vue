<template>
  <div>
    <button class="btn btn-outline-light" @click="togglePanel">
      <i class="bi bi-person-wheelchair bi-gger"></i>
    </button>
    <transition name="slide">
      <div v-if="open" class="access-panel shadow-lg p-3">
        <h5>Dostępność</h5>

        <div class="mt-3">
          <p class="fw-bold">Rozmiar czcionki:</p>
          <button class="btn btn-secondary btn-sm me-2" @click="changeFont(-1)">A-</button>
          <button class="btn btn-secondary btn-sm" @click="changeFont(1)">A+</button>
        </div>

        <div class="mt-3">
          <p class="fw-bold">Kontrast:</p>
          <button class="btn btn-dark btn-sm" @click="toggleContrast">
            Wysoki kontrast
          </button>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const open = ref(false);
const togglePanel = () => (open.value = !open.value);

function changeFont(delta: number) {
  const html = document.documentElement;
  const current = parseFloat(getComputedStyle(html).fontSize);
  html.style.fontSize = current + delta + "px";
}

function toggleContrast() {
  document.body.classList.toggle("high-contrast");
}
</script>

<style>
.access-panel {
  position: fixed;
  right: 0;
  top: 0;
  width: 260px;
  height: 100vh;
  background: white;
  z-index: 3000;
}

.slide-enter-from,
.slide-leave-to {
  transform: translateX(100%);
}
.slide-enter-to,
.slide-leave-from {
  transform: translateX(0);
}
.slide-enter-active,
.slide-leave-active {
  transition: transform 0.3s ease;
}

.high-contrast {
  filter: invert(1) contrast(1.5);
}
</style>
