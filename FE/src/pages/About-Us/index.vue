<script setup></script>

<template>
  <div class="about-us">
    <div class="about-us-title">
      <h1>Về chúng tôi!!!</h1>
    </div>
    <HfOurTeam />
    <HfFooter />
  </div>
</template>

<style lang="scss" scoped>
.about-us {
  .about-us-title {
    background: url('@/assets/images/introduce-bg.jpg') no-repeat;
    height: 70vh;
    background-size: cover;
    background-position: center;
    position: relative;
    @include flex;
    &::before {
      content: '';
      position: absolute;
      top: 0;
      right: 0;
      left: 0;
      bottom: 0;
      background-color: #00000094;
      z-index: 4;
    }
  }
  h1 {
    font-weight: 600;
    font-size: 4rem;
    text-align: center;
    margin-bottom: 40px;
    color: #fff;
    position: relative;
    z-index: 40;
    @include mobile {
      word-wrap: break-word;
    }
  }
}
</style>
<route lang="yaml">
name: About-Us
meta:
  layout: default
</route>
