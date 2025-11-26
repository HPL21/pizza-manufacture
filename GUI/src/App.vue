<template>
  <Navbar />
  <router-view />
  <Footer />
  <Toast />
</template>

<script setup lang="ts">
import Navbar from "./components/Navbar.vue";
import Toast from "./components/Toast.vue";
import Footer from "./components/Footer.vue";
</script>

<style>
</style>
