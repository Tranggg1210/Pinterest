<script setup>
import { getCollectionByUserId } from '@/api/collection.api';
import { getAllPostByUserId } from '@/api/post.api';
import { useCurrentUserStore } from '@/stores/currentUser';
import { useMessage } from 'naive-ui';
const posts = ref([]);
const user = useCurrentUserStore();
const message = useMessage();
const loading = ref(true);
const isActiveButton = ref(true);
const tableRef = ref([]);
const numberSlides = ref(3);
const loadPosts = async () => {
  loading.value = true;
  try {
    const result = await getAllPostByUserId();
    posts.value = result;
    loading.value = false;
    isActiveButton.value = true;
    } catch (err) {
    loading.value = false;
    console.log(err);
    message.error("Tải danh sách bài viết không thành công");
  }
};
const loadCollections = async() => {
  try {
    loading.value = true;
    const result = await getCollectionByUserId();
    tableRef.value = result;
    loading.value = false;
    isActiveButton.value = false;
    message.success("Tải danh sách bảng thành công")
  } catch (error) {
    console.log(error);
    loading.value = false;
    message.error("Tải danh sách bảng thất bại")
  }
}
console.log(tableRef);
const updateNumberSlides = () => {
  const width = window.innerWidth;
  if (width < 700) {
    numberSlides.value = 1;
  } else if (width < 870) {
    numberSlides.value = 2;
  } else if (width < 1200) {
    numberSlides.value = 3;
  } else {
    numberSlides.value = 3;
  }
};
onMounted(() => {
  window.addEventListener('resize', updateNumberSlides);
});
onUnmounted(() => {
  window.removeEventListener('resize', updateNumberSlides);
});

onBeforeMount(async () => {
  updateNumberSlides();
  await loadPosts();
});
</script>
<template>
  <div class="profile-favorite container">
    <div class="wide">
      <div class="basic-profile container">
        <div class="user-avatar">
          <img
            :src="user.currentUser.avatar"
            alt="avatar"
            v-if="user.currentUser.avatar"
          />
          <img
            src="@/assets/images/user-avatar.png"
            alt="avatar"
            v-else
          />
        </div>
        <h1 class="user-name">
          {{ user.currentUser.fullname || "Nguyễn Thị Trang" }}
        </h1>
        <p class="user-account">
          <IconBrandPinterest size="20" />
          {{ user.currentUser.username || "Nguyễn Thị Trang" }}
        </p>
        <div class="btn-container">
          <HfButton :class="isActiveButton && 'active'" @click="loadPosts">Đã tạo</HfButton>
          <HfButton :class="!isActiveButton && 'active'" @click="loadCollections">Đã lưu</HfButton>
        </div>
        <div v-if="isActiveButton">
          <HfLoading v-if="loading"/>
          <div v-else class="container">
            <div class="wide posts-container" v-if="posts.length > 0">
              <HfPost v-for="post in posts" :key="post.id" :postInfor="post" :isEdit="true"/>
            </div>
            <HfNoData v-else/>
          </div>
        </div>
        <div v-else>
          <HfLoading v-if="loading"/>
          <div v-else>
            <div v-if="true">
              <n-space vertical class="table-slider">
                <n-carousel
                class="carousel"
                autoplay
                show-dots
                :interval="3000"
                :space-between="30"
                loop
                :slides-per-view="numberSlides"
                draggable
                >
                  <div v-for="(tableInfor, index) in tableRef">
                    <hf-card-collection :class="`collection-item-${index % 6 + 1}`" :key="index" :tableInfor="tableInfor"/>
                  </div>
                 <template #dots="{ total, currentIndex, to }">
                  <hf-custom-dots :total="total" :currentIndex="currentIndex" @to="to"></hf-custom-dots>
                </template>
                </n-carousel>
              </n-space>
              <div class="posts-container"></div>
            </div>
            <HfNoData v-else/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped src="./Profile-Favorite.scss">

</style>
<route lang="yaml">
name: ProfileFavorite
meta:
  layout: default
  requiresAuth: true
</route>
