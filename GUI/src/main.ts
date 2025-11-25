import { createApp } from "vue"
import { createPinia } from "pinia"
import { useAuthStore } from "./stores/auth";

import App from "./App.vue"
import router from "./router"
import DataTable from 'datatables.net-vue3'
import DataTablesLib from 'datatables.net'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.bundle.min.js"
import 'datatables.net-bs5/css/dataTables.bootstrap5.css'

import "./assets/custom_style.css"

DataTable.use(DataTablesLib)

const app = createApp(App)

app.use(createPinia())
app.use(router)

const auth = useAuthStore();
auth.loadFromStorage();
app.component('DataTable', DataTable)
app.mount("#app")
