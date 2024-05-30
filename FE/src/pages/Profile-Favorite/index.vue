<script setup>
import { getCollectionByUserId } from '@/api/collection.api';
import { getAllPostByUserId } from '@/api/post.api';
import { useCurrentUserStore } from '@/stores/currentUser';
import { useMessage } from 'naive-ui';
import { onMounted, onUnmounted, onBeforeMount, ref } from 'vue';

const posts = ref([]);
const user = useCurrentUserStore();
const message = useMessage();
const loading = ref(true);
const isActiveButton = ref(true);
const tableRef = ref([]);
const numberSlides = ref(3);

const fetchData = async (fetchFunction, dataRef, successMessage, errorMessage) => {
  loading.value = true;
  try {
    const result = await fetchFunction();
    dataRef.value = result;
    message.success(successMessage);
  } catch (err) {
    console.error(err);
    message.error(errorMessage);
  } finally {
    loading.value = false;
  }
};

const loadPosts = () => {
  isActiveButton.value = true;
  fetchData(
    getAllPostByUserId,
    posts,
    'Tải danh sách bài viết thành công',
    'Tải danh sách bài viết không thành công'
  );
};

const loadCollections = () => {
  isActiveButton.value = false;
  fetchData(
    getCollectionByUserId,
    tableRef,
    'Tải danh sách bảng và danh sách bài viết của bảng mặc định thành công',
    'Tải danh sách bảng và danh sách bài viết của bảng mặc định thất bại'
  );
};

const updateNumberSlides = () => {
  const width = window.innerWidth;
  numberSlides.value = width < 700 ? 1 : width < 870 ? 2 : width < 1200 ? 2 : 3;
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
const removePost = (postId) => {
  posts.value = posts.value.filter((post) => post.id !== postId);
};
</script>

<template>
  <div class="profile-favorite container">
    <div class="wide">
      <div class="basic-profile container">
        <div class="user-avatar">
          <img :src="user.currentUser.avatar" alt="avatar" v-if="user.currentUser.avatar"/>
          <img src="@/assets/images/user-avatar.png" alt="avatar" v-else/>
        </div>
        <h1 class="user-name">{{ user.currentUser.fullname || 'Nguyễn Thị Trang' }}</h1>
        <p class="user-account">
          <IconBrandPinterest size="20" />
          {{ user.currentUser.username || 'Nguyễn Thị Trang' }}
        </p>
        <div class="btn-container">
          <HfButton :class="{ active: isActiveButton }" @click="loadPosts">Đã tạo</HfButton>
          <HfButton :class="{ active: !isActiveButton }" @click="loadCollections">Đã lưu</HfButton>
        </div>
        <div v-if="isActiveButton">
          <HfLoading v-if="loading" />
          <div v-else class="container">
            <div class="wide posts-container" v-if="posts.length">
              <HfPost
                v-for="post in posts"
                :key="post.id"
                :postInfor="post"
                :isEdit="true"
                @deletePost="removePost"
                @updatePosts="loadPosts"
              />
            </div>
            <HfNoData v-else />
          </div>
        </div>
        <div v-else>
          <HfLoading v-if="loading" />
          <div v-else>
            <div v-if="tableRef.length">
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
                  <HfCardCollection
                    v-for="(tableInfor, index) in tableRef"
                    :class="`collection-item-${(index % 6) + 1}`"
                    :key="tableInfor.id"
                    :tableInfor="tableInfor"
                  />
                  <template #dots="{ total, currentIndex, to }">
                    <hf-custom-dots
                      :total="total"
                      :currentIndex="currentIndex"
                      @to="to"
                    ></hf-custom-dots>
                  </template>
                </n-carousel>
              </n-space>
            </div>
            <HfNoData v-else />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped src="./Profile-Favorite.scss"></style>

<route lang="yaml">
name: ProfileFavorite
meta:
  layout: default
  requiresAuth: true
</route>
