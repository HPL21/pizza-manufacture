import { createApp } from "vue"
import { createPinia } from "pinia"

import App from "./App.vue"
import router from "./router"

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"

import "./assets/custom_style.css"

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount("#app")
