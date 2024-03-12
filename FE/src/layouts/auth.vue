<template>
  <div class="auth-layout">
    <div class="auth-card">
      <div @click="$router.back" class="close-form">
        <icon-x />
      </div>
      <router-view></router-view>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>
