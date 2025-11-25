import { createRouter, createWebHistory } from "vue-router"
import { useAuthStore } from "@/stores/auth"
import HomeView from "@/views/HomeView.vue"

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: "/", name: "home", component: HomeView },
    {
      path: "/login",
      name: "login",
      component: () => import("@/views/LoginView.vue")
    },
    {
      path: "/register",
      name: "register",
      component: () => import("@/views/RegisterView.vue")
    },
    {
      path: "/order",
      name: "order",
      component: () => import("@/views/OrderView.vue"),
      meta: { requiresAuth: true }
    },
    {
      path: "/order-summary",
      name: "orderSummary",
      component: () => import("@/views/OrderSummaryView.vue"),
      meta: { requiresAuth: true }
    },
    {
      path: "/admin",
      name: "admin",
      component: () => import("@/views/AdminView.vue"),
      meta: { requiresAuth: true }
    },
    {
      path: "/ingredients",
      name: "ingredients",
      component: () => import("@/views/IngredientView.vue")
    },
    {
      path: "/pizza",
      name: "pizza",
      component: () => import("@/views/PizzaView.vue")
    }
  ]
})

router.beforeEach((to, from, next) => {
  const auth = useAuthStore()

  if ((to.path === "/login" || to.path === "/register") && auth.isLogged) {
    next("/");
    return;
  }

  if (to.meta.requiresAuth && !auth.isLogged) {
    next("/login")
  } else {
    next()
  }
})

export default router
