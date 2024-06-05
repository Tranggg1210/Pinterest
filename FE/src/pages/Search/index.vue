<script setup>
import { ref, watch, onBeforeMount } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { searchPosts } from '@/api/post.api';
import { useMessage } from 'naive-ui';

const router = useRouter();
const route = useRoute();
const message = useMessage();
const posts = ref([]);
const loading = ref(false);
const keyword = ref(route.query.q || localStorage.getItem('keyword') || '');

const goBack = () => {
  const previousPath = localStorage.getItem('previousPath');
  localStorage.removeItem('keyword');
  localStorage.removeItem('previousPath');
  if (previousPath) {
    router.push(previousPath);
  } else {
    router.push('/');
  }
};

const handleSearchValue = async () => {
  try {
    loading.value = true;
    const result = await searchPosts({
      keyword: keyword.value
    });
    posts.value = result.data.posts;
    loading.value = false;
  } catch (error) {
    loading.value = false;
    message.error('Tìm kiếm thất bại');
    console.error(error);
  }
};

watch(
  () => route.query.q,
  async (newQuery) => {
    if (newQuery) {
      keyword.value = newQuery;
      await handleSearchValue();
    }
  }
);

onBeforeMount(handleSearchValue);
</script>

<template>
  <div class="container">
    <div class="wide">
      <div @click="goBack">
        <IconArrowLeft class="icon icon-back" />
      </div>
      <h1 class="title">
        Kết quả tìm kiếm
        <template v-if="keyword">
          về <span>{{ keyword.toLowerCase() }}</span>
        </template>
      </h1>
      <div class="container" v-if="loading">
        <HfLoading />
      </div>
      <div v-else>
        <div class="posts-container" v-if="posts.length > 0">
          <HfPostSearch
            v-for="(post, index) in posts"
            :key="index"
            :postInfor="post"
            :isNotDefault="fasle"
          />
        </div>
        <HfNoData v-else />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.title {
  margin: 32px 0;
  font-weight: 600;
  color: #333;
  text-align: center;
  span {
    color: $primary-color;
    text-decoration: underline;
  }
}
.icon-back {
  position: fixed;
  top: 100px;
  left: 44px;
  width: 44px;
  height: 44px;
  padding: 8px;
  border-radius: 50%;
  transition: all 0.3s linear;
  background-color: #fff;
  z-index: 100;
  &:hover {
    background-color: #ccc;
  }
  @include mobile {
    display: none;
  }
  @include small-tablet {
    top: 86px;
    left: 16px;
  }
  @include tablet {
    left: 24px;
  }
}
</style>

<route lang="yaml">
name: Search
meta:
  layout: default
  requiresAuth: true
</route>
