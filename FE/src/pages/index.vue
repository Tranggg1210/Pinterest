<script setup>
import axios from 'axios';
import { onBeforeMount } from 'vue';
const posts = ref([]);
const loadPosts = async() => {
  try {
    const {data} = await axios.get('https://picsum.photos/v2/list?page=2&limit=100');
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
  <div class="container">
    <div class="wide posts-container" >
      <HfPost  v-for="post in posts" :key="post.id" :postInfor="post"/>
    </div>
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
