import { defineStore } from "pinia";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    isLogged: false,
    isAdminUser: false,
    username: ""
  }),

  actions: {
    // TODO: IMPLEMENT
    login() {
      this.isLogged = true;
      this.username = "Użytkownik";
      this.isAdminUser = true;
    },

    logout() {
      this.isLogged = false;
      this.username = "";
      this.isAdminUser = false;
    },

    setAdmin(value: boolean) {
      this.isAdminUser = value;
    }
  }
});
