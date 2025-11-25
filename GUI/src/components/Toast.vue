<template>
  <transition name="toast-slide">
    <div
      v-if="notif.visible"
      class="toast-container px-4 py-2 bg-danger text-white rounded shadow"
      role="status"
      aria-live="polite"
    >
      {{ notif.message }}
    </div>
  </transition>
</template>

<script setup lang="ts">
import { useNotificationStore } from "../stores/notification";
const notif = useNotificationStore();
</script>

<style scoped>
.toast-container {
  position: fixed;
  top: 76px;
  left: 50%;
  transform: translateX(-50%);
  z-index: 2000;
  pointer-events: auto;
}

.toast-slide-enter-from {
  transform: translateX(-50%) translateY(calc(-100% - 20px));
  opacity: 0;
}
.toast-slide-enter-active {
  transition: transform 0.5s ease, opacity 0.45s ease;
}
.toast-slide-enter-to {
  transform: translateX(-50%) translateY(0);
  opacity: 1;
}

.toast-slide-leave-from {
  transform: translateX(-50%) translateY(0);
  opacity: 1;
}
.toast-slide-leave-active {
  transition: transform 0.5s ease, opacity 0.45s ease;
}
.toast-slide-leave-to {
  transform: translateX(-50%) translateY(calc(-100% - 20px));
  opacity: 0;
}

.toast-container {
  min-width: 200px;
  max-width: 90vw;
  text-align: center;
  box-shadow: 0 6px 18px rgba(0,0,0,0.2);
}
</style>
