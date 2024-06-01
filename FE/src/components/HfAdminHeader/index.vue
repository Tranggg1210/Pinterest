<script setup>
import { useCurrentUserStore } from '@/stores/currentUser';

  const currentU = useCurrentUserStore();
</script>
<template>
  <div class="header">
    <RouterLink
      style="display: flex; justify-content: space-between; align-items: center; cursor: pointer"
      to="/user-infor"
    >
      <div class="avatar">
        <img src="@/assets/images/user-avatar.png" alt="avatar" class="user-avatar" v-if="!currentU.currentUser.avatar"/>
        <img :src="currentU.currentUser.avatar" alt="avatar" class="user-avatar" v-else/>
      </div>
      <span>Admin!</span>
    </RouterLink>
  </div>
</template>

<style lang="scss" scoped>
.header {
  padding: 12px 24px;
  box-shadow: 0 0 12px rgba(0 0 0/10%);
  @include flex(end, center);
  img {
    width: 36px;
    height: 36px;
  }
  a:hover {
    opacity: 0.6;
  }
  .avatar{
    height: 36px;
    width: 36px;
    border-radius: 50%;
    overflow: hidden;
    margin-right: 6px;
    @include mobile {
      width: 28px;
      height: 28px;
    }
  }
}
</style>
