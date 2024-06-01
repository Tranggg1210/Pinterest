<script setup>
import { getAnalysisToday } from '@/api/admin.api';
import { useCurrentUserStore } from '@/stores/currentUser';
import { useMessage } from 'naive-ui';
import { onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const user = useCurrentUserStore();
const message = useMessage();
const options = ref([]);
const loading = ref(false);
const goToPage = () => {
  router.push('/');
};
const loadAnalysisToday = async() => {
  try {
    loading.value = true;
    const result = await getAnalysisToday();
    options.value = [
      {
        id: 1,
        title: 'Số lượt truy cập trong ngày',
        quality: result?.accessCountInDay,
        desc: 'lượt truy cập'
      },
      {
        id: 2,
        title: 'Số lượng người dùng',
        quality: result?.userCount,
        desc: 'người dùng'
      },
      {
        id: 3,
        title: 'Số lượng bài viết',
        quality: result?.postCount,
        desc: 'bài viết'
      },
      {
        id: 4,
        title: 'Số lượng thông báo',
        quality: result?.notificationCount,
        desc: 'thông báo'
      }
    ];
    loading.value = false;
  } catch (error) {
    console.log(error);
    loading.value = false;
    message.error("Tải thông tin thất bại")
  }
}
onBeforeMount(loadAnalysisToday);
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
    <div class="card-quality" v-if="!loading">
      <div v-if="options.length > 0">
        <HfCardHomeAdmin
          v-for="item in options"
          :class="`collection-item-${item.id}`"
          :key="item.id"
          :data="item"
        />
      </div>
      <HfNoData v-else></HfNoData>
    </div>
    <HfLoading v-else/>
  </div>
</template>

<style lang="scss" scoped src="./Admin.scss"></style>
<route lang="yaml">
name: Admin
meta:
  layout: admin
</route>
