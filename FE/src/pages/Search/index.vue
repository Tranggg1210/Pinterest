<script setup>
import { ref } from 'vue';
const active = ref(false);
const placement = ref('right');
const activate = (place) => {
  active.value = true;
  placement.value = place;
};
</script>

<template>
  <n-button-group>
    <n-button @click="activate('top')"> Top </n-button>
  </n-button-group>
  <n-drawer v-model:show="active" :width="502" :placement="placement">
    <n-drawer-content title="Stoner">
      Stoner is a 1965 novel by the American writer John Williams.
    </n-drawer-content>
  </n-drawer>

  <div :show="searchActive" class="model"></div>
  <div class="search">
    <div class="search__container"></div>
  </div>
  <div class="post__container">
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
  grid-row-gap: 60px;
  grid-column-gap: 20px;
  padding: 0 5%;
}
</style>

<route lang="yaml">
name: Search
meta:
  layout: default
</route>
