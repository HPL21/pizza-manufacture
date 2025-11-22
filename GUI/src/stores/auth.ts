import { defineStore } from "pinia";
import api from "../api";

function parseJwt(token: string) {
  try {
    return JSON.parse(atob(token.split(".")[1]!));
  } catch (e) {
    return null;
  }
}

export const useAuthStore = defineStore("auth", {
  state: () => ({
    isLogged: false,
    isAdminUser: false,
    username: "",
    token: ""
  }),

  actions: {
    async login(username: string, password: string) {
      try {
        const res = await api.post("/api/Account/login", {
          username,
          password
        });

        this.token = res.data.token;
        this.username = res.data.username;
        this.isAdminUser = res.data.role === "Admin";
        this.isLogged = true;

        localStorage.setItem("auth_token", this.token);
        localStorage.setItem("auth_user", this.username);
        localStorage.setItem("auth_admin", String(this.isAdminUser));
      } catch (err: any) {
        throw new Error(err.response?.data || "Błąd logowania");
      }
    },

    async register(username: string, email: string, password: string) {
      try {
        const res = await api.post("/api/Account/register", {
          username,
          email,
          password
        });

        return res.data;
      } catch (err: any) {
        throw new Error(err.response?.data || "Błąd rejestracji");
      }
    },

    logout() {
      this.isLogged = false;
      this.username = "";
      this.isAdminUser = false;
      this.token = "";

      localStorage.removeItem("auth_token");
      localStorage.removeItem("auth_user");
      localStorage.removeItem("auth_admin");
    },

    loadFromStorage() {
      const token = localStorage.getItem("auth_token");
      const username = localStorage.getItem("auth_user");

      if (!token) return;

      const payload = parseJwt(token);
      const now = Math.floor(Date.now() / 1000);

      if (!payload || payload.exp < now) {
        this.logout();
        return;
      }

      this.token = token;
      this.username = username || "";
      this.isAdminUser = localStorage.getItem("auth_admin") === "true";
      this.isLogged = true;
    }
  }
});
