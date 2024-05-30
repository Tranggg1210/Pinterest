<script setup>
import { onBeforeMount } from 'vue';
import { useAuthStore } from '@/stores/auth';
import HfFeature from '@/components/HfFeature/HfFeature.vue';
import { getAllPost } from '@/api/post.api';
const posts = ref([]);
const user = useAuthStore();
const message = useMessage();
const loading = ref(true);

const loadPosts = async () => {
  loading.value = true;
  try {
    const result = await getAllPost();
    posts.value = result;
    loading.value = false;
  } catch (err) {
    loading.value = false;
    console.log(err);
    message.error('Tải danh sách bài viết không thành công');
  }
};
onBeforeMount(() => {
  if (user.loggedIn) {
    loadPosts();
  }
});
const goToDetailProduct = (id) => {
  router.push(`/detail-post/${id}`);
};
</script>
<template>
  <div v-if="user.loggedIn">
    <HfLoading v-if="loading" />
    <div v-else class="container">
      <div class="wide posts-container" v-if="posts.length > 0">
        <HfPost v-for="post in posts" :key="post.id" :postInfor="post" :isEdit="false" />
      </div>
      <div v-else>
        <HfNoData />
      </div>
    </div>
  </div>
  <div v-else>
    <HfBanner />
    <HfFeature />
    <HfShowCase />
    <HfFooter />
  </div>
</template>

<style scoped lang="scss">
.posts-container {
  margin: 30px 0;
}
</style>

<route lang="yaml">
name: Home
meta:
  layout: default
</route>
