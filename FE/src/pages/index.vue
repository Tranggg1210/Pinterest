<script setup>
import { onBeforeMount } from 'vue';
import { useAuthStore } from '@/stores/auth';
import HfFeature from '@/components/HfFeature/HfFeature.vue';
import { getAllPost } from '@/api/post.api';
const posts = ref([]);
const user = useAuthStore();
const message = useMessage();
const loadPosts = async() => {
  try {
    const {data} = await getAllPost();
    posts.value = data;
  } catch (err) {
    console.log(err);
    if (!!err.response) {
      message.error(err.response.data.title);
    } else {
      message.error(err.title);
    }
  }
}
onBeforeMount(loadPosts);
</script>
<template>
  <div class="container" v-if="user.loggedIn">
    <div class="wide posts-container" >
      <HfPost  v-for="post in posts" :key="post.id" :postInfor="post"/>
    </div>
  </div>
  <div v-else>
    <HfBanner/>
    <HfFeature/>
    <HfShowCase/>
    <HfFooter/>
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
