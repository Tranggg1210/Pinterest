<template>
  <div class="container">
    <div class="wide">
      <img src="@/assets/images/no-data.jpg" alt="" />
    </div>
  </div>
</template>

<script setup></script>

<style lang="scss" scoped>
.wide {
  @include flex(center, center);
}
img {
  width: 40%;
  @include mobile {
    width: 80%;
  }
  @include small-tablet {
    width: 80%;
  }
}
</style>
