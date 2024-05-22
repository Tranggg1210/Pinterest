<script setup>
import { useMessage } from 'naive-ui';
const loading = ref(false);
</script>
<template>
  <div class="collection-favorite container">
    <div class="wide">
      <div class="basic-collection container">
        <div class="collection-background">
          <!-- <img
            :src=""
            alt="background"
            v-if=""
          /> -->
          <img
            src="@/assets/images/collection.png"
            alt="background"
            style="width: 72%;"
          />
        </div>
        <h1 class="collection-name">
          {{ "Nguyễn Thị Trang" }}
        </h1>
        <p class="collection-account">
          <IconSum size="20" />
          {{ "0" }} bài viết
        </p>
        <div>
          <HfLoading v-if="loading"/>
          <div v-else class="container">
            <!-- <div class="wide posts-container">
            </div> -->
            <HfNoData />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.collection-favorite {
  margin-top: 40px;
}
.basic-collection {
  flex-direction: column;
  align-items: center;
  .collection-background {
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
  .collection-name {
    margin: 16px 0 0;
    color: #333;
    font-weight: 500;
  }
  .collection-account {
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
path: '/detail-collection/:id'
name: DetailCollection
meta:
  layout: default
  requiresAuth: true
</route>
