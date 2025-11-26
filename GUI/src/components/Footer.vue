<template>
  <footer class="footer bg-custom-dark text-light">
    <span>© 2025 JKK Production</span>

    <nav class="footer-nav">
      <router-link to="/about" class="footer-link">O nas</router-link>
      <span class="separator">•</span>
      <router-link to="/contact" class="footer-link">Kontakt</router-link>
    </nav>
  </footer>
</template>

<style scoped>
.footer {
  text-align: center;
  padding: 20px;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.footer-nav {
  display: flex;
  justify-content: center;
  gap: 16px;
  align-items: center;
}

.footer-link {
  color: #ddd;
  text-decoration: none;
  font-weight: 500;
}

.footer-link:hover {
  text-decoration: underline;
  color: #fff;
}

.separator {
  opacity: 0.6;
}
</style>
