<script setup>
import {useLoadingBar, useMessage } from 'naive-ui';
import { onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';
import {getUserById} from '@/api/user.api.js';
import {getAllPostByUserId} from '@/api/post.api.js';

const message = useMessage();
const loading = ref(false);
const router = useRouter();
const posts = ref([]);
const user = ref({});

const loadUserArticles = async() => {
    try {
        const data = await getUserById(router.currentRoute.value.params.id);
        user.value = data;
        const result = await getAllPostByUserId(router.currentRoute.value.params.id);
        posts.value = result;
    } catch (error) {
        console.log(error);
        message.error("Không thể lấy dữ liệu của người dùng này");
    }
}
const handleFullName = (firstName, lastName) => {
  const fullName = `${lastName} ${firstName} `;
  const formattedFullName = fullName
    .replace(/\s+/g, ' ')
    .trim()
    .replace(/(^|\s)\S/g, (match) => match.toUpperCase());

  return formattedFullName;
};
onBeforeMount(loadUserArticles)
</script>
<template>
  <div>
    <div class="user-favorite container">
    <HfLoading v-if="loading"/>
      <div class="wide" v-else>
        <div class="basic-user container">
          <div class="user-background">
            <img
              :src="user?.avatarUrl"
              alt="background"
              v-if="user?.avatarUrl"
            />
            <img
              v-else
              src="@/assets/images/user-avatar.png"
              alt="background"
              style="width: 72%;"
            />
          </div>
          <h1 class="user-name">
            {{ user?.firstName ? handleFullName(user?.firstName, user?.lastName) : "Chưa xác định" }}
          </h1>
          <div class="user-account">
            <p>
                <IconUsers size="20" />
                {{ user?.follower || "0"  }} người theo dõi
            </p>
            <p>
                <IconUserStar size="20" />
                {{ user?.following || "0"  }} người đang theo dõi
            </p>
          </div>
          <div>
            <div class="container">
              <div class="wide posts-container" v-if="posts.length > 0">
                <HfPost v-for="post in posts" :key="post.id" :postInfor="post" />
              </div>
              <HfNoData v-else />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.user-favorite {
  margin-top: 40px;
}
.basic-user {
  flex-direction: column;
  align-items: center;
  .user-background {
    width: 110px;
    height: 110px;
    border-radius: 50%;
    border: 5px solid #2eca7f;
    background-color: #2eca7f;
    overflow: hidden;
    cursor: pointer;
    transition: all 0.3s linear;
    @include flex;
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
    >p{
        padding: 0 16px;
         @include flex(center, center);

    }
    @include mobile{
        flex-direction: column;
    }
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
.posts-input{
  border: 1px solid #ccc;
}
.container-end{
  display: flex;
  justify-content: end;
}
</style>
<route lang="yaml">
path: '/user-articles/:id'
name: UserArticles
meta:
  layout: default
  requiresAuth: true
</route>
