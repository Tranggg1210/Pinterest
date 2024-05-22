<script setup>
const { tableInfor } = defineProps(['tableInfor']);
</script>
<template>
  <div class="slider-item">
    <RouterLink :to="`/detail-collection/${tableInfor?.id}`">
      <div class="slider-content">
          <h3>{{ tableInfor?.name || "BẢNG KHÔNG CÓ TÊN" }}</h3>
          <p>{{ tableInfor?.postCount || "0" }} bài viết</p>
      </div>
      <div class="slider-img">
          <img src="@/assets/images/picture.png" alt="slider-image">
      </div>
    </RouterLink>
  </div>
</template>

<style lang="scss" scoped>
.slider-item{
  background-color: #18975F;
  padding: 24px;
  width: 100% !important;
  border-radius: 20px;
  a{
    display: block;
    @include flex;
  }
  .slider-img{
    margin: 0 24px;
    width: 100px;
    height: 100px; 
    background-color: #ffdaad;
    border-radius: 50%;
    @include flex;
    img{
      width: 90px;
      margin-right: 4px;
    }
    @include mobile{
      width: 60px;
      height: 60px;
      margin: 0;
      img{
        width: 60px;
        margin: 0;
      }
    }
  }
  .slider-content{
    flex-grow: 1;
    color: #fff;
    padding: 0 24px;
    h3{
      font-size: 16px;
      font-weight: 600;
      line-height: 1.4;
      text-transform: uppercase;
      margin-bottom: 8px;
    }
    p{
      font-size: 14px;
    }
    @include mobile{
      padding: 0;
    }
  }
  transition: all 0.3s linear;
  &:hover{
    opacity: 0.8;
  }
}
</style>
