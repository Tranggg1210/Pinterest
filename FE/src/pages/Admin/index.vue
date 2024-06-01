<script setup>
import { useCurrentUserStore } from '@/stores/currentUser';
import { useRouter } from 'vue-router';

const router = useRouter();
const user = useCurrentUserStore();
const options = [
  {
    id: 1,
    title: 'Số lượt truy cập trong ngày',
    quality: 0,
    desc: 'lượt truy cập'
  },
  {
    id: 2,
    title: 'Số lượng người dùng',
    quality: 0,
    desc: 'người dùng'
  },
  {
    id: 3,
    title: 'Số lượng bài viết',
    quality: 0,
    desc: 'bài viết'
  },
  {
    id: 4,
    title: 'Số lượng thông báo',
    quality: 0,
    desc: 'thông báo'
  }
];
const goToPage = () => {
  router.push('/');
};
</script>
<template>
  <div class="home-admin">
    <div class="home-banner container">
      <div class="basic-user">
        <div class="user-background">
          <img :src="user?.currentUser.avatar" alt="background" v-if="user?.currentUser.avatar" />
          <img v-else src="@/assets/images/user-avatar.png" alt="background" style="width: 72%" />
        </div>
        <h1 class="user-name">
          {{ user?.currentUser.fullname !== 'Null Null' ? user.currentUser.fullname  : 'Admin' }}
        </h1>
        <n-button type="warning" @click="goToPage">Về trang chủ của thành viên</n-button>
      </div>
    </div>
    <div class="card-quality">
      <div>
        <HfCardHomeAdmin
          v-for="item in options"
          :class="`collection-item-${item.id}`"
          :key="item.id"
          :data="item"
        />
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped src="./Admin.scss"></style>
<route lang="yaml">
name: Admin
meta:
  layout: admin
</route>
