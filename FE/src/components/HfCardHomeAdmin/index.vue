<script setup>
const { data } = defineProps(['data']);
</script>
<template>
  <div class="slider-item">
    <div>
      <div class="slider-content">
          <h3>{{ data?.title || "KHÔNG XÁC ĐỊNH" }}</h3>
          <p>{{ data?.quality || "0" }} {{ data?.desc || "không xác định"}}</p>
      </div>
      <div class="slider-img">
          <img v-if="data.id === 1" src="@/assets/images/portfolio.png" alt="image">
          <img v-else-if="data.id === 2" src="@/assets/images/social-media.png" alt="image">
          <img v-else-if="data.id === 3" src="@/assets/images/picture.png" alt="image">
          <img v-else-if="data.id === 4" src="@/assets/images/notification-admin.png" alt="image">
          <img v-else src="@/assets/images/picture.png" alt="image">
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.slider-item{
  background-color: #18975F;
  padding: 24px;
  width: 48% !important;
  border-radius: 20px;
  @include mobile{
    width: 100% !important;
  }
  @include small-tablet{
    width: 100% !important;
  }
  >div{
    display: block;
    @include flex;
    @include mobile{
      flex-direction: column-reverse;
      text-align: center;
    }
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
      margin: 0 0 20px 0;
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
