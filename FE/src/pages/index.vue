<script setup>
import { onBeforeMount } from 'vue';
import { useAuthStore } from '@/stores/auth';
import HfFeature from '@/components/HfFeature/HfFeature.vue';
import { getAllPost } from '@/api/post.api';
import router from '@/router';
const posts = ref([]);
const user = useAuthStore();
const message = useMessage();
const loadPosts = async () => {
  try {
    const result = await getAllPost();
    posts.value = result;
  } catch (err) {
    console.log(err);
    if (!!err.response) {
      message.error(err.response.data.message);
    } else {
      message.error(err.message);
    }
  }
};
onBeforeMount(() => {
  if (user.loggedIn) {
    loadPosts();
  }
});
console.log(posts);
const goToDetailProduct = (id) => {
  router.push(`/detail-post/${id}`)
}
</script>
<template>
  <div class="container" v-if="user.loggedIn">
    <div class="wide posts-container" v-if="posts.length > 0">
      <HfPost v-for="post in posts" :key="post.id" :postInfor="post" @click="() => goToDetailProduct(post.id)" />
    </div>
    <div v-else>
      <HfNoData />
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
