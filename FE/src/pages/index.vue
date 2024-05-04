<script setup>
import axios from 'axios';
import { onBeforeMount } from 'vue';
import { useAuthStore } from '@/stores/auth';
import HfFeature from '@/components/HfFeature/HfFeature.vue';
const posts = ref([]);
const user = useAuthStore();
const loadPosts = async() => {
  try {
    const {data} = await axios.get('https://picsum.photos/v2/list?page=2&limit=40');
    posts.value = data;
  } catch (err) {
    console.log(err);
    if (!!err.response) {
      message.error(err.response.data.message);
    } else {
      message.error(err.message);
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
