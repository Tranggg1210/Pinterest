
<script setup>
defineProps({
  total: Number,
  currentIndex: Number
});
defineEmits(['to']);
</script>
<template>
  <ul class="custom-dots">
    <li
      class="custom-dots-li"
      v-for="index of total"
      :key="index"
      :class="{ ['is-active']: currentIndex === index - 1 }"
      @click="$emit('to', index - 1)"
    />
  </ul>
</template>
<style lang="scss" scoped>
.custom-dots {
  display: flex;
  margin: 0 auto;
  padding: 0;
  position: absolute;
  bottom: 0px;
  z-index: 200;
  left: 50%;
  transform: translateX(-50%);
}

.custom-dots li {
  display: inline-block;
  width: 12px;
  height: 4px;
  margin: 0 3px;
  border-radius: 4px;
  background-color: #de731c7a;
  transition: width 0.3s, background-color 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  cursor: pointer;
}

.custom-dots li.is-active {
  width: 40px;
  background: #f06c25 !important;
}
</style>
