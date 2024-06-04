<script setup>
  import { ref, onMounted, onBeforeMount } from 'vue';
  import { useCommentStore } from '@/stores/commentStore';
  import { getUserById } from '@/api/user.api';
  import { useRouter } from 'vue-router';
  import { useCurrentUserStore } from '@/stores/currentUser';
  
  const { commentItem } = defineProps(['commentItem']);
  const currentUser = useCurrentUserStore();
  const commentStore = useCommentStore();
  const user = ref({});
  const router = useRouter();
  const isCurrentUser = ref(false);
  const showModal = ref(false);
  const commentUpdate = ref({});
  const options = ref([
  {
    label: 'Trả lời',
    key: 1,
    disabled: true
  },
  {
    label: "Chỉnh sửa",
    key: 2
  },
  {
    label: 'Xóa',
    key: 3
  }
]);
const optionsNotCurrentUser = [
  {
    label: 'Trả lời',
    key: 1,
    disabled: true
  }
];
  const rules = {
    content: {
      required: true,
      validator: (_, content) => (content?.trim() ? true : new Error('Vui lòng nhập nội dung bình luận cần sửa!')),
      trigger: ['blur', 'input'],
    },
  };
  const loadUser = async () => {
    try {
      const result = await getUserById(commentItem?.UserId);
      user.value = result;
      if (user.value.userName === currentUser.currentUser.username) {
        isCurrentUser.value = true;
      } else {
        isCurrentUser.value = false;
      }
    } catch (error) {
      console.error('Lỗi khi tải thông tin user:', error);
    }
  };
  
  onBeforeMount(loadUser);
  
  function getFormattedDate(dateTimeString) {
  if (!dateTimeString) {
    return '';
  }
  
  const parts = dateTimeString.split(' ');
  if (parts.length < 1) {
    return '';
  }
  
  const datePart = parts[0];
  const dateParts = datePart.split('-');
  if (dateParts.length < 3) {
    return '';
  }

  const year = dateParts[0];
  const month = dateParts[1];
  const day = dateParts[2];

  return `${day}-${month}-${year}`;
}

  
  const handleSelect = (key) => {
    if (key === 2) {
      console.log(commentItem);
      commentUpdate.value = { content: commentItem.Content }
      showModal.value = true;
    } else if (key === 3) {
      commentStore.deleteComment(commentItem.Id);
    }
  };
  
  const gotoPage = () => {
    router.push(`/user-articles/${commentItem.UserId}`);
  };
  
  const handleUpdate = () => {
    commentStore.updateComment({ commentId: commentItem.Id, content: commentUpdate.value.content });
    showModal.value = false;
  };
  
</script>
<template>
    <div class="comemnt-item">
      <div class="user-avatar">
        <img
          src="@/assets/images/user-avatar.png"
          alt="avatar"
          v-if="!user?.avatarUrl"
        />
        <img :src="user?.avatarUrl" alt="avatar" class="user-avatar" v-else />
      </div>
      <div class="user-comment">
        <div class="comment-title">
          <h4 class="user-name" @click="gotoPage" v-if="!isCurrentUser">{{ user.firstName }}</h4>
          <h4 class="user-name" v-else>{{ user.firstName }}</h4>
          <p>{{ commentItem.Content }}</p>
        </div>
        <div class="comment-create">
          <p>{{ getFormattedDate(commentItem.CreatedAt) }}</p>
          <n-dropdown trigger="hover" :options="isCurrentUser ? options : optionsNotCurrentUser" @select="handleSelect">
            <IconDots size="20" class="icon"/>
          </n-dropdown>
        </div>
      </div>
      <n-modal
        v-model:show="showModal"
        class="custom-card"
        preset="card"
        title="Chỉnh sửa bình luận"
        style="width: 50%"
        :bordered="false"
      >
        <n-form ref="formRef" :model="commentUpdate" :rules="rules" size="large">
          <n-form-item label="Nội dung bình luận" path="content">
            <n-input v-model:value="commentUpdate.content" placeholder="Nội dung bình luận" class="input" />
          </n-form-item>
          <n-form-item class="container-end">
            <n-button @click="() => showModal = false">Hủy</n-button>
            <n-button type="success" style="color: white; margin-left: 12px" @click="handleUpdate">
              Chỉnh sửa bình luận
            </n-button>
          </n-form-item>
        </n-form>
      </n-modal>
    </div>
  </template>
  <style lang="scss" scoped>
  .comemnt-item {
    margin: 20px 0;
    width: 100%;
    @include flex(space-between, center);
  }
  
  .user-comment {
    flex-grow: 1;
    margin-left: 12px;
  }
  
  .user-avatar {
    width: 32px;
    height: 32px;
    border-radius: 50%;
    overflow: hidden;
    > img {
      width: 100%;
    }
  }
  
  .user-name {
    cursor: pointer;
    &:hover {
      color: $primary-color;
    }
  }
  
  .comment-title {
    @include flex(unset, start);
    flex-wrap: wrap;
    p {
      margin: 0;
      margin-left: 8px;
    }
  }
  
  .comment-create {
    @include flex(unset, center);
    p {
      font-size: 13px;
      color: #7e7e7e;
      margin: 0;
    }
    .icon {
      margin-left: 8px;
      &:hover {
        color: red !important;
      }
    }
  }
  </style>
  
