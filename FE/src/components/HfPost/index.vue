<script setup>
import { ref } from 'vue';
</script>

<template>
  <div class="post__item">
    <div class="post-image">
      <img src="https://i.pinimg.com/736x/a4/c7/13/a4c713ee683ff455464faa9132a69688.jpg" alt="" />

      <div class="model">
        <div class="model__header">
          <p class="colection-name">Miss girl <IconChevronDown class="icon"></IconChevronDown></p>
          <button class="btn-post-save">Lưu</button>
        </div>
        <div class="model__footer">
          <IconShare2 class="icon"></IconShare2>
          <IconDownload class="icon"></IconDownload>
        </div>
      </div>
    </div>

    <div class="post__info">
      <div class="post__info-top">Thu Hà cutee (✿◕‿◕✿) 🐰 🌷☘️ 🌷</div>
      <div class="post__info-bottom">
        <div class="post-avatar"><img src="../../assets//images//logo.png" alt="" /></div>
        <div class="post-username">haniekid</div>
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
  min-height: 120px;
  margin-bottom: 12px;
  .post-image {
    position: relative;
    height: 80%;
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

  .post__info {
    height: 20%;
    .post__info-bottom {
      display: flex;
      align-items: center;
      .post-avatar {
        width: 40px;
        height: 40px;
        border-radius: 100%;
      }
      .post-username {
        line-height: 50px;
      }
    }

    .post__info-top {
      margin-top: 10px;
      margin-bottom: -4px;
      font-weight: 600;
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
    width: 35px;
    height: 35px;
    background-color: #f7f7f7;
    border-radius: 100%;
  }
}
</style>
