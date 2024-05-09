<script setup></script>
<template>
  <button class="btn">
    <slot />
  </button>
</template>

<style lang="scss" scoped>
.btn {
  padding: 12px 16px;
  margin: 0 12px;
  background-color: $secondary-color;
  border-radius: 32px;
  border: none;
  outline: none;
  font-weight: 500;
  font-size: 15px;
  cursor: pointer;
  transition: all 0.4s linear;
  text-wrap: nowrap;
  &:hover {
    background-color: $black-color;
    color: $white-color;
    opacity: 0.8;
  }
}
</style>
