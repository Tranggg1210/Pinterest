<script setup></script>

<template>
  <div class="detail-post">
    <IconArrowLeft class="icon"></IconArrowLeft>
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
              Miss girl<IconChevronDown class="icon"></IconChevronDown>
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
          </div>
          <div class="user-name">hanikid</div>
        </div>

        <div class="detail-right-comment">
          <div class="title">Nhận xét</div>
          <IconChevronUp class="icon"></IconChevronUp>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.icon {
  margin: 20px 5% 20px;
  width: 30px;
  height: 30px;
}

.detail-post {
  width: 100%;
  height: 100vh;

  .detail-post-container {
    display: flex;

    margin: auto;
    box-shadow: 5px 5px 12px 5px #ededed;
    width: 50%;
    min-height: 550px;
    border-radius: 35px;
    .detail-post-left {
      width: 50%;
      height: 100%;
      img {
        width: 100%;
        height: 100%;
        object-fit: cover;
        border-radius: 35px;
      }
    }
    .detail-post-right {
      width: 50%;
      padding-left: 5%;
      padding-top: 30px;
      .detail-right-option {
        display: flex;
        justify-content: space-between;
      }
    }
  }
}

.option-left {
  display: flex;
  .icon {
    margin-right: 13px;
  }
}

.collection-name {
  display: flex;
  align-items: center;
  min-width: 200px;
  font-weight: 600;
  font-size: 17px;
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

.detail-right-title {
  font-size: 28px;
  font-weight: 600;
  margin: 10px 0;
}
.detail-right-des {
  font-size: 17px;
  margin-top: 10px;
}
.detail-right-user {
  display: flex;
  align-items: center;
  margin-top: 30px;
  .user-avatar {
    width: 50px;
    height: 50px;
    border-radius: 100rem;
    margin-right: 10px;
    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      border-radius: 100rem;
    }
  }
  .user-name {
  }
}
.detail-right-comment {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-top: 50px;
  width: 100%;
  font-size: 18px;
  font-weight: 600;
}
</style>

<route lang="yaml">
name: DetailPost
meta:
  layout: default
</route>
