<script setup></script>

<template>
  <div class="post__container">
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
    <HfPost></HfPost>
  </div>
</template>

<style scoped lang="scss">
.post__container {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  grid-column-gap: 20px;
  padding: 0 5%;
  padding-top: 30px;

  @include mobile {
    grid-template-columns: repeat(1, 1fr);
  }

  @include small-tablet {
    grid-template-columns: repeat(3, 1fr);
  }
}
</style>

<route lang="yaml">
name: Home
meta:
  layout: default
</route>
