import { createRouter, createWebHistory } from "vue-router"
import { useAuthStore } from "@/stores/auth"
import HomeView from "@/views/HomeView.vue"

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { 
      path: "/",
      name: "home",
      meta: { title: "Strona główna" },
      component: HomeView 
    },
    {
      path: "/login",
      name: "login",
      meta: { title: "Logowanie" },
      component: () => import("@/views/LoginView.vue")
    },
    {
      path: "/register",
      name: "register",
      meta: { title: "Rejestracja" },
      component: () => import("@/views/RegisterView.vue")
    },
    {
      path: "/about",
      name: "about",
      meta: { title: "O nas" },
      component: () => import("@/views/AboutView.vue")
    },
    {
      path: "/contact",
      name: "contact",
      meta: { title: "Kontakt" },
      component: () => import("@/views/ContactView.vue")
    },
    {
      path: "/order",
      name: "order",
      component: () => import("@/views/OrderView.vue"),
      meta: { requiresAuth: true, title: "Zamówienie" }
    },
    {
      path: "/order-summary",
      name: "orderSummary",
      component: () => import("@/views/OrderSummaryView.vue"),
      meta: { requiresAuth: true, title: "Podsumowanie zamówienia"  }
    },
    {
      path: "/admin",
      name: "admin",
      component: () => import("@/views/AdminView.vue"),
      meta: { requiresAuth: true, title: "Panel administratora"  }
    },
    {
      path: "/ingredients",
      name: "ingredients",
      component: () => import("@/views/IngredientView.vue"),
      meta: { requiresAuth: true, title: "Składniki"  }
    },
    {
      path: "/pizza",
      name: "pizza",
      component: () => import("@/views/PizzaView.vue"),
      meta: { requiresAuth: true, title: "Pizze"  }
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
