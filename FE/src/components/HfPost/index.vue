<script setup>
import { defineProps, onBeforeMount, ref } from 'vue';
const {postInfor} = defineProps(['postInfor']);
const imageURL = ref('')

const handleURLImage = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    imageURL.value =  URL.createObjectURL(blob);
  } catch (error) {
    console.error('Lỗi khi tải ảnh:', error);
  }
};
onBeforeMount(() => handleURLImage(postInfor.download_url))
</script>

<template>
  <div class="post__item">
    <div class="post-image">
      <img :src="postInfor.thumbnailUrl" alt="image" loading="lazy" />
      <div class="model">
        <div class="model__header">
          <button class="btn-post-save">Lưu</button>
        </div>
        <div class="model__footer">
          <IconShare2 class="icon" size="12"></IconShare2>
          <a download :href="imageURL" title="ImageName">
            <IconDownload class="icon" size="12" ></IconDownload>
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.icon {
  cursor: pointer;
}
.post__item {
  z-index: 1;
  min-height: 100px;
  .post-image {
    position: relative;
    height: 100%;
    width: 100%;

    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      border-radius: 25px;
    }

    &:hover .model {
      visibility: visible;
      background-color: rgba(0, 0, 0, 0.4);
      cursor: pointer;
    }
    .model {
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      z-index: 100;
      visibility: hidden;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      border-radius: 25px;
    }
  }

}
img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}
.model__header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  color: white;
  font-weight: 700;
  font-size: 15px;
  padding: 10px 15px;
  .colection-name {
    display: flex;
    justify-content: center;
    .icon {
      margin-left: 5px;
    }
  }

  .btn-post-save {
    background-color: red;
    color: white;
    font-weight: 700;
    font-size: 15px;
    border-radius: 20px;
    padding: 14px 18px;
    border: none;
    cursor: pointer;
  }
}

.model__footer {
  position: absolute;
  float: right;
  right: 10px;
  bottom: 5px;
  .icon {
    padding: 5px 5px;
    color: #000;
    width: 32px;
    height: 32px;
    background-color: #f7f7f7;
    border-radius: 100%;
  }
}
</style>
