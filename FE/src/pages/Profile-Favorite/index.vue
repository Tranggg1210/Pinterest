<script setup>
import { getAllPostByUserId } from '@/api/post.api';
import { useCurrentUserStore } from '@/stores/currentUser';
import { useMessage } from 'naive-ui';
import { RouterLink } from 'vue-router';
const posts = ref([]);
const router = useRouter();
const user = useCurrentUserStore();
const message = useMessage();
const loading = ref(true);
const loadPosts = async () => {
  loading.value = true;
  try {
    const result = await getAllPostByUserId();
    posts.value = result;
    loading.value = false;
    } catch (err) {
    loading.value = false;
    console.log(err);
    message.error("Tải danh sách bài viết không thành công");
  }
};
onBeforeMount(loadPosts);
</script>
<template>
  <div class="profile-favorite container">
    <div class="wide">
      <div class="basic-profile container">
        <div class="user-avatar">
          <img
            :src="user.currentUser.avatar"
            alt="avatar"
            v-if="user.currentUser.avatar"
          />
          <img
            src="@/assets/images/user-avatar.png"
            alt="avatar"
            v-else
          />
        </div>
        <h1 class="user-name">
          {{ user.currentUser.fullname || "Nguyễn Thị Trang" }}
        </h1>
        <p class="user-account">
          <IconBrandPinterest size="20" />
          {{ user.currentUser.username || "Nguyễn Thị Trang" }}
        </p>
        <div class="btn-container">
          <HfButton class="active">Đã tạo</HfButton>
          <HfButton >Đã lưu</HfButton>
        </div>
        <HfLoading v-if="loading"/>
        <div v-else class="container">
          <div class="wide posts-container" v-if="posts.length > 0">
            <HfPost v-for="post in posts" :key="post.id" :postInfor="post" :isEdit="true" @click="() => goToDetailProduct(post.id)" />
          </div>
          <HfNoData v-else/>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.profile-favorite {
  margin-top: 40px;
}
.basic-profile {
  flex-direction: column;
  align-items: center;
  .user-avatar {
    width: 110px;
    height: 110px;
    border-radius: 50%;
    border: 5px solid #2eca7f;
    overflow: hidden;
    cursor: pointer;
    transition: all 0.3s linear;
    > img {
      width: 100%;
    }
    &:hover {
      scale: 0.9;
    }
  }
  .user-name {
    margin: 16px 0 0;
    color: #333;
    font-weight: 500;
  }
  .user-account {
    @include flex(center, center);
    color: #717171;
    margin-bottom: 18px;
  }
  .btn-container {
    @include mobile {
      button {
        width: 100%;
        margin: 0 0 16px;
      }
    }
  }
}
.active {
  background-color: #000;
  color: #fff;
}
.posts-container{
  margin: 40px 0;
}
</style>
<route lang="yaml">
name: ProfileFavorite
meta:
  layout: default
  requiresAuth: true
</route>
