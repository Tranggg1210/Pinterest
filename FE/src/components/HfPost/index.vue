<script setup>
import { ref } from 'vue';
</script>

<template>
  <div class="post__item">
    <img src="../../assets//images//logo.png" alt="" />

    <div class="model">
      <div class="model__header">
        <p class="colection-name">Miss girl <IconCaretDown class="icon"></IconCaretDown></p>
        <button class="btn-post-save">Lưu</button>
      </div>
      <div class="model__footer">
        <IconShare2 class="icon"></IconShare2>
        <IconCaretDown class="icon"></IconCaretDown>
      </div>
    </div>

    <div class="post__info">
      <div class="post-avatar"><img src="../../assets//images//logo.png" alt="" /></div>
      <div class="post-username">haniekid</div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.icon {
  cursor: pointer;
}
.post__item {
  position: relative;
  background-color: #ccc;
  height: 100px;
  border-radius: 25px;
  z-index: 1;
  height: 280px;
  &:hover .model {
    visibility: visible;
  }
  img {
    width: 100%;
    height: auto;
    object-fit: cover;
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
    background-color: transparent;
    /* background-color: pink; */
    opacity: 0.9;
  }

  .post__info {
    display: flex;
    /* flex-direction: column; */
    /* justify-content: space-between; */
    .post-avatar {
      width: 50px;
      height: 50px;
      border-radius: 100%;
    }
    .post-username {
      line-height: 50px;
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
  padding: 10px 15px;
  color: white;
  font-weight: 700;
  font-size: 15px;
  .colection-name {
    display: flex;
    justify-content: center;
  }
  .btn-post-save {
    background-color: red;
    color: white;
    font-weight: 700;
    font-size: 15px;
    border-radius: 20px;
    padding: 14px 21px;
    border: none;
    cursor: pointer;
  }
}

.model__footer {
  position: absolute;
  float: right;
  right: 20px;
  bottom: 10px;
}
</style>
