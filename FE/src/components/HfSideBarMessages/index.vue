<script setup>
import { useRouter } from 'vue-router';
import { onBeforeMount } from 'vue';
import { useConversationStore } from '@/stores/conversationStore';
import { useLoadingBar, useMessage } from 'naive-ui';

const router = useRouter();
const conversationStore = useConversationStore();
const inputValue = ref('');
const loading = ref(false);
const message = useMessage();
const loadingBar = useLoadingBar();
console.log(conversationStore.conversations);
const goBack = () => {
  router.push('/');
};

const handleFullName = (firstName, lastName) => {
  return `${lastName} ${firstName}`.replace(/\s+/g, ' ').trim().replace(/(^|\s)\S/g, (match) => match.toUpperCase());
};

const loadConversations = async () => {
  if (!conversationStore.conversations.length) {
    loading.value = true;
    try {
      await conversationStore.loadConversation();
    } catch (error) {
      message.error("Lỗi, không thể tải các cuộc hội thoại");
    } finally {
      loading.value = false;
    }
  }
};

const debouncedHandleSearch = async () => {
  try {
    if(inputValue.value)
    {
      loadingBar.start();
      await conversationStore.handleSearchValue(inputValue.value);
      loadingBar.finish();
      inputValue.value = '';
    }else{
      message.warning("Bạn chưa nhập thông tin tìm kiếm")
    }
  } catch (error) {
    message.error('Tìm kiếm thất bại');
    loadingBar.error();
  } finally {
    loadingBar.finish();
  }
};


onBeforeMount(loadConversations);
</script>

<template>
  <div>
    <div class="sidebar">
      <div class="sidebar-header">
        <IconArrowLeft size="20" class="icon-back" @click="goBack" />
        <p>Hộp thư đến</p>
      </div>
      <div class="sidebar-search">
        <div>
          <input type="text" v-model="inputValue" placeholder="Tìm kiếm theo email" @keypress.enter="debouncedHandleSearch"/>
          <IconSearch size="24" @click="debouncedHandleSearch" />
        </div>
      </div>
      <div class="sidebar-body">
        <nav v-if="!loading">
          <ul v-if="conversationStore.conversations.length > 0">
            <li v-for="user in conversationStore.conversations" :key="user.Id">
              <RouterLink :to="`/detail-message/${user.ConnectorId}`" exact-active-class="active">
                <div class="user-avatar">
                  <img src="@/assets/images/user-avatar.png" alt="avatar" v-if="!user?.avatarUrl" />
                  <img :src="user?.avatarUrl" alt="avatar" class="user-avatar" v-else />
                </div>
                <div>
                  <p>{{ user.UserName }}</p>
                  <p class="username">{{ handleFullName(user.FirstName, user.LastName) }}</p>
                </div>
              </RouterLink>
            </li>
          </ul>
        </nav>
        <HfLoading v-else />
      </div>
    </div>
    <div class="cushion"></div>
  </div>
</template>

<style lang="scss" scoped>
.sidebar {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  width: 320px;
  background-color: #fff;
  box-shadow: 0 0 12px rgba(0 0 0 / 15%);
  height: 100vh;
}
.cushion {
  width: 320px;
}
.sidebar-header {
  padding: 20px;
  position: relative;
  p {
    text-align: center;
    margin: 0;
    font-weight: 500;
  }
  .icon-back {
    position: absolute;
    cursor: pointer;
    &:hover {
      color: red;
    }
  }
}
.sidebar-search {
  padding: 12px 20px 0;
  > div {
    border: 2px solid #ccc;
    @include flex(space-between, center);
    padding: 8px 12px 8px 16px;
    border-radius: 40px;
    &:hover {
      border: 2px solid red;
    }
  }
  input {
    outline: none;
    border: none;
  }
}
.sidebar-body {
  padding: 20px;
  margin: 0 -8px;
  li {
    a {
      @include flex(left, center);
      padding: 8px;
      cursor: pointer;
      &:hover {
        background-color: #e9e9e9;
        border-radius: 4px;
      }
    }
    .active {
      background-color: #e9e9e9;
      border-radius: 4px;
    }
    p {
      margin: 0;
    }
  }
  .icon-edit {
    width: 40px;
    height: 40px;
    background-color: red;
    border-radius: 50%;
    @include flex(center, center);
    margin-right: 8px;
    .icon { 
      color: #fff;
      margin: 0;
    }
  }
}
.user-avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  overflow: hidden;
  margin-right: 8px;
  > img {
    width: 100%;
  }
}
.username {
  font-size: 13px;
  color: #6c6c6c;
}
</style>
