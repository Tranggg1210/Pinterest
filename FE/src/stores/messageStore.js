import { defineStore } from 'pinia';

export const useMessageStore = defineStore({
  id: 'message',
  state: () => ({
    messagesValue: [],
  }),
  actions: {
    async loadMessages(messages) {
      this.messagesValue = messages;
    },
    async addMessage(message) {
      this.messagesValue.push(message);
    },
    async clearMessages() {
      this.messagesValue = [];
    },
  },
});
