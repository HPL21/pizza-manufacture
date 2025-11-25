import { defineStore } from "pinia";

export const useNotificationStore = defineStore("notification", {
  state: () => ({
    message: "" as string,
    visible: false
  }),

  actions: {
    show(msg: string, timeout = 3000) {
      this.message = msg;
      this.visible = true;

      setTimeout(() => {
        this.visible = false;
      }, timeout);
    }
  }
});
