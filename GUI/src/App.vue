<template>
  <Navbar />
  <router-view />
  <Toast />
</template>

<script setup lang="ts">
import Navbar from "./components/Navbar.vue";
import Toast from "./components/Toast.vue";
</script>

<style>
</style>
