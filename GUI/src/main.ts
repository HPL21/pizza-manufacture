import { createApp } from "vue"
import { createPinia } from "pinia"
import { useAuthStore } from "./stores/auth";

import App from "./App.vue"
import router from "./router"

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"

import "./assets/custom_style.css"

const app = createApp(App)

app.use(createPinia())
app.use(router)

const auth = useAuthStore();
auth.loadFromStorage();

app.mount("#app")
