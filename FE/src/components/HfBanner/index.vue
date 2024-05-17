<script setup></script>
<template>
  <div class="home-banner">
    <div class="banner-content">
      <h1>Welcome to PixelPalette!!!</h1>
      <hr class="line" />
    </div>
  </div>
</template>

<style lang="scss" scoped>
.home-banner {
  height: calc(100vh - 76px);
  background: url('@/assets/images/banner-bg.jpg') no-repeat;
  background-size: cover;
  background-position: bottom;
  position: relative;
  .banner-content {
    position: absolute;
    top: 8%;
    left: 0;
    right: 0;
    @include flex;
    flex-direction: column;
    @include mobile {
      top: 4%;
    }
    @include small-tablet {
      top: 2%;
    }
  }
  h1 {
    color: rgb(0, 118, 211);
    font-size: 52px;
    font-weight: 600;
    text-align: center;

    @include mobile {
      padding: 0 24px;
      font-size: 40px;
    }
    @include small-tablet {
      font-size: 44px;
    }
  }
  .line {
    border: none;
    border-top: 4px dotted rgb(97, 140, 123);
    width: 20%;
    margin: 20px 0 0;
  }
  @include mobile {
    height: 60vh;
  }
  @include small-tablet {
    height: 48vh;
  }
  @include tablet {
    height: 48vh;
  }
}
</style>
