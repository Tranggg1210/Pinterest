<script setup>
import { useCommentStore } from '@/stores/commentStore';
import { useCurrentUserStore } from '@/stores/currentUser';
import { ref } from 'vue';
const { postId } = defineProps(['postId']);
const commentStore = useCommentStore();
const currentU = useCurrentUserStore();
const inputValue = ref('');
const submitComment = () => {
  commentStore.createComment({
    postId,
    content: inputValue.value
  });
  inputValue.value = '';
};
</script>

<template>
  <div class="user">
    <div class="user-avatar">
      <img src="@/assets/images/user-avatar.png" alt="avatar" v-if="!currentU.currentUser.avatar" />
      <img :src="currentU.currentUser.avatar" alt="avatar" class="user-avatar" v-else />
    </div>
    <div class="comment-input">
      <input
        type="text"
        placeholder="Thêm nhận xét"
        v-model="inputValue"
        @keypress.enter="submitComment"
      />
      <IconSend2 size="24" @click="submitComment" class="icon-send" />
    </div>
  </div>
</template>

<style scoped lang="scss">
.user {
  @include flex(space-between, center);
  margin: 16px 0 32px;
}
.user-avatar {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  overflow: hidden;
  > img {
    width: 100%;
  }
}
.comment-input {
  flex-grow: 1;
  margin-left: 12px;
  border: 2px solid #ccc;
  border-radius: 40px;
  @include flex(center, center);
  overflow: hidden;
  padding: 2px 12px;
  > input {
    width: 100%;
    height: 48px;
    border: none;
    font-size: 16px;
    outline: none;
    padding-left: 4px;
  }
  &:hover {
    border: 2px solid chocolate;
  }
}
.icon-send {
  cursor: pointer;
}
</style>
