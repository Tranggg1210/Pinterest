<template>
  <div class="messager">
    <HfSideBarMessages class="messager-sidebar" />
    <div class="messager-right">
      <RouterView></RouterView>
    </div>
  </div>
</template>
<style scoped lang="scss">
.messager {
  display: flex;
  justify-content: center;
  align-items: center;
  &-right {
    flex-grow: 1;
  }
}
</style>
