<script setup>
import { ref, onBeforeMount, watch, nextTick, onMounted } from 'vue';
import debounce from 'lodash/debounce';
import { useRouter } from 'vue-router';
import { useCurrentUserStore } from '@/stores/currentUser';
import { useConversationStore } from '@/stores/conversationStore';
import { useMessageStore } from '@/stores/messageStore';
import { useMessage } from 'naive-ui';
import { getAllConversation, getMessages, sendMessages } from '@/api/chat.api';
import { getUserById } from '@/api/user.api';

const router = useRouter();
const currentU = useCurrentUserStore();
const messages = useMessage();
const inputValue = ref('');
const idConversation = ref(-1);
const loading = ref(false);
const conversationsStore = useConversationStore();
const conversationList = ref([]);
const messagesBody = ref(null);
const creatingMessage = ref(false);
const user = ref({});

const messageStore = useMessageStore();

const loadUser = async () => {
  try {
    const result = await getUserById(router.currentRoute.value.params.id);
    user.value = result;
  } catch (error) {
    console.error('Error loading user:', error);
  }
};

const loadMessages = async () => {
  try {
    const result = await getAllConversation();
    conversationList.value = result.data;
    const itemConversation = conversationList.value.find(
      (item) => item.ConnectorId === router.currentRoute.value.params.id
    );
    if (itemConversation) {
      idConversation.value = itemConversation?.Id;
      if (idConversation.value !== -1) {
        const data = await getMessages(itemConversation.Id);
        messageStore.loadMessages(data.data);
      }
    }
  } catch (error) {
    console.error('Error loading messages:', error);
    messages.error('Lỗi không thể tải messages');
  }
};

const debouncedLoadUserAndMessages = debounce(() => {
  loadUser();
  loadMessages();
}, 300);

watch(
  () => router.currentRoute.value.params.id,
  debouncedLoadUserAndMessages
);

watchEffect(async () => {
  if (messagesBody.value) {
    messagesBody.value.scrollTop = messagesBody.value.scrollHeight;
  }
});

onBeforeMount(async () => {
  try {
    loading.value = true;
    await loadUser();
    await loadMessages();
  } catch (error) {
    console.error('Error on before mount:', error);
    messages.error('Lỗi không thể tải thông tin người dùng này');
    router.push('/messages');
  } finally {
    loading.value = false;
  }
});

const handleSendMessages = async () => {
  try {
    if (!inputValue.value.trim()) {
      messages.warning('Bạn chưa soạn tin nhắn');
      return;
    }
    if (idConversation.value !== -1) {
      const newMessage = {
        content: inputValue.value,
        senderId: currentU.currentUser.id,
        timestamp: new Date().toISOString(),
      };
      await sendMessages(newMessage, idConversation.value);
      messageStore.addMessage(newMessage);
      nextTick(() => {
        if (messagesBody.value) {
          messagesBody.value.scrollTop = messagesBody.value.scrollHeight;
        }
      });
      inputValue.value = '';
    }
  } catch (error) {
    console.error('Error sending messages:', error);
    messages.error('Gửi tin nhắn thất bại');
  }
};

const handleCancelCreateConversation = async () => {
  try {
    await conversationsStore.loadConversation();
    idConversation.value = -1;
    router.push('/messages');
  } catch (error) {
    console.error('Error canceling conversation creation:', error);
  }
};
</script>

<template>
  <div class="messages" v-if="!loading">
    <div class="messages-header">
      <n-button type="warning" v-if="idConversation === -1" @click="handleCancelCreateConversation">
        Hủy tạo cuộc trò chuyện này
      </n-button>
      <div v-else></div>
      <div class="user-header">
        <div class="user-avatar">
          <img src="@/assets/images/user-avatar.png" alt="avatar" v-if="!user?.avatarUrl" />
          <img :src="user?.avatarUrl" alt="avatar" class="user-avatar" v-else />
        </div>
        <div>
          <p>{{ user?.userName }}</p>
        </div>
      </div>
    </div>
    <div class="messages-body" ref="messagesBody">
      <div v-if="messageStore.messagesValue.length > 0">
        <HfMessagesItem v-for="(item, index) in messageStore.messagesValue" 
          :key="index"
          :messageData="item"
          :user="user"
        />
      </div>
    </div>
    <div class="messages-footer">
      <div class="user">
        <div class="user-avatar">
          <img src="@/assets/images/user-avatar.png" alt="avatar" v-if="!currentU.currentUser.avatar" />
          <img :src="currentU.currentUser.avatar" alt="avatar" class="user-avatar" v-else />
        </div>
        <div class="comment-input" v-if="idConversation === -1">
          <input type="text" placeholder="Nhắn tin" v-model="inputValue" @keypress.enter="handleSendMessages" />
          <IconSend2 size="24" @click="handleSendMessages" class="icon-send" />
        </div>
        <div class="comment-input" v-else>
          <input type="text" placeholder="Nhắn tin" :disabled="creatingMessage" v-model="inputValue" @keypress.enter="handleSendMessages" />
          <IconSend2 size="24" class="icon-send" />
        </div>
      </div>
    </div>
  </div>
  <HfLoading v-else></HfLoading>
</template>

<style lang="scss" scoped src="./DetailMessage.scss">
</style>

<route lang="yaml">
path: '/detail-message/:id'
name: DetailMessage
meta:
  layout: empty
  requiresAuth: true
</route>
