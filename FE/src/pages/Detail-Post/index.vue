<script setup>
import { useRouter } from 'vue-router';
const router = useRouter()

</script>
<template>
  <div class="container">
    <div class="wide">
      <div class="detail-post">
        <RouterLink to="/">
          <IconArrowLeft class="icon icon-back"/>
        </RouterLink>
        <div class="detail-post-container">
          <div class="detail-post-left">
            <img src="../../assets/images/test94.jpg" alt="" />
          </div>
          <div class="detail-post-right">
            <div class="detail-right-option">
              <div class="option-left">
                <IconShare2 class="icon"></IconShare2>
                <IconDownload class="icon"></IconDownload>
              </div>
              <div class="option-right">
                <div class="collection-name">
                  <button class="btn-post-save">Lưu</button>
                </div>
              </div>
            </div>

            <div class="detail-right-title">Tiêu đề đâyyy</div>
            <div class="detail-right-des">
              Mô tả đây: 👉 Lorem ipsum dolor sit amet consectetur, adipisicing elit. 👈
            </div>
            <div class="detail-right-user">
              <div class="user-avatar">
                <img src="../../assets/images/test6.jpg" alt="" />
                <p class="user-name">hanikid</p>
              </div>
              <HfButton class="btn-follow">Theo dõi</HfButton>
            </div>

            <div class="detail-right-comment">
              <div class="title">Nhận xét</div>
              <IconChevronUp class="icon"></IconChevronUp>
            </div>
          </div>
        </div>
      </div>
      <div class="relate-posts">
        <h3>Thêm nội dung để khám phá</h3>
        <div class="posts-container">
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
          <HfPost/>
        </div>
      </div>
    </div>
  </div>
 
</template>

<style scoped lang="scss" src="./DetailPost.scss">

</style>

<route lang="yaml">
path: '/detail-post/:id'
name: DetailPost
meta:
  layout: default
</route>
