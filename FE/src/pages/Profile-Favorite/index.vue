<script setup>
const dataImage = [];
const router = useRouter();
</script>
<template>
  <div class="profile-favorite container">
    <div class="wide">
      <div class="basic-profile container">
        <div class="user-avatar">
          <img
            src="https://i.pinimg.com/280x280_RS/63/1e/53/631e53700ce9c9bbdc5c8d529956fc29.jpg"
            alt=""
          />
        </div>
        <h1 class="user-name">Nguyễn Thị Trang</h1>
        <p class="user-account">
          <IconBrandPinterest size="20" />
          nguyenthitrang.ttd
        </p>
        <div class="btn-container">
          <HfButton>Tạo ghim</HfButton>
          <HfButton @click="router('/')">Chỉnh sửa hồ sơ</HfButton>
          <HfButton class="active">Đã lưu</HfButton>
          <HfButton>Đã tạo</HfButton>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.profile-favorite {
  margin-top: 40px;
}
.basic-profile {
  flex-direction: column;
  align-items: center;
  .user-avatar {
    width: 110px;
    height: 110px;
    border-radius: 50%;
    border: 4px solid rgb(131, 173, 251);
    overflow: hidden;
    cursor: pointer;
    transition: all 0.3s linear;
    > img {
      width: 100%;
    }
    &:hover {
      scale: 0.9;
    }
  }
  .user-name {
    margin: 16px 0 0;
    color: #333;
    font-weight: 500;
  }
  .user-account {
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
</style>
<route lang="yaml">
name: ProfileFavorite
meta:
  layout: default
</route>
