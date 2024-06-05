import { defineStore } from 'pinia';
import { ref } from 'vue';
import { searchPosts } from '@/api/post.api';
import { getAllConversation, createConversation } from '@/api/chat.api';
import { getUserById, searchUser } from '@/api/user.api';

export const useConversationStore = defineStore({
  id: 'conversation',
  state: () => ({
    conversations: []
  }),
  actions: {
    async loadConversation() {
      try {
        const result = await getAllConversation();
        this.conversations = result.data;

        if (this.conversations.length > 0) {
          await this.loadUserDetails();
        }
      } catch (error) {
        console.error('Lỗi khi tải thông tin user:', error);
      }
    },
    async loadUserDetails() {
      try {
        const userPromises = this.conversations.map((item) => getUserById(item.ConnectorId));
        const userData = await Promise.all(userPromises);

        this.conversations = this.conversations.map((conversation, index) => ({
          ...conversation,
          avatarUrl: userData[index].avatarUrl,
          FirstName: userData[index].firstName,
          LastName: userData[index].lastName,
          UserName: userData[index].userName
        }));
      } catch (error) {
        console.error('Error loading user details:', error);
      }
    },
    async handleSearchValue(inputValue) {
      try {
        const result = await searchUser(inputValue);
        this.conversations = result.data.users;
        if (this.conversations.length > 0) {
          const userPromises = this.conversations.map((item) => getUserById(item.Id));
          try {
            const userData = await Promise.all(userPromises);
            userData.forEach((data, index) => {
              this.conversations[index].avatarUrl = data.avatarUrl;
              this.conversations[index].ConnectorId = data.id;
            });
          } catch (error) {
            console.error('Lỗi khi tải thông tin user:', error);
          }
        }
      } catch (error) {
        console.error(error);
      }
    },
    async createConversationById(id, inputValue) {
      try {
        await createConversation(id, { content: inputValue });
      } catch (error) {
        console.error('Error creating conversation:', error);
      }
    }
  }
});
