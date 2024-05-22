<script setup>
import { getCollectionById } from '@/api/collection.api';
import { getPostByCollectionId } from '@/api/post.api';
import { useMessage } from 'naive-ui';
import { onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';

const message = useMessage();
const loading = ref(false);
const router = useRouter();
const collectionInfor = ref({});
const posts = ref([]);
const options = [
    {
      label: "Câp nhập hình nền",
      key: 1,
    },
    {
      label: "Cập nhập bộ sưu tập",
      key: 2
    },
    {
      label: "Xóa bộ sưu tập",
      key: 4
    }
];

const loadCollection = async() => {
  try {
    const result = await getCollectionById(router.currentRoute.value.params.id);
    collectionInfor.value = result;
  } catch (error) {
    console.log(error);
    message.error("Lỗi không thể tải được dữ liệu của collection");
  }
}
const loadPostOfCollection = async() => {
  try {
    loading.value = true;
    const result = await getPostByCollectionId(collectionInfor.value.id);
    posts.value = result;
    loading.value = false;
    console.log(result);
  } catch (error) {
    console.log(error);
    loading.value = false;
    message.error("Lỗi không thể tải được danh sách bài viết của collection");
  }
}
onBeforeMount(async() => {
  await loadCollection();
  await loadPostOfCollection();
});
const handleName = (name) => {
  const formattedName = name
    .replace(/\s+/g, ' ')
    .trim()
    .replace(/(^|\s)\S/g, (match) => match.toUpperCase());

  return formattedName;
};
</script>
<template>
  <div class="collection-favorite container">
    <div class="wide">
      <div class="basic-collection container">
        <div class="collection-background">
          <img
            :src="collectionInfor?.backgroundUrl"
            alt="background"
            v-if="collectionInfor?.backgroundUrl"
          />
          <img
            v-else
            src="@/assets/images/collection.png"
            alt="background"
            style="width: 72%;"
          />
        </div>
        <h1 class="collection-name">
          {{ collectionInfor?.name ? handleName(collectionInfor?.name) : "Chưa xác định" }}
        </h1>
        <p class="collection-account">
          <IconFileDescription size="20" />
          {{ collectionInfor?.description || "Chưa có mô tả"  }}
        </p>
        <n-space>
          <n-dropdown trigger="click" :options="options">
            <n-button>Chỉnh sửa bộ sưu tập</n-button>
          </n-dropdown>
        </n-space>
        <div>
          <HfLoading v-if="loading"/>
          <div v-else class="container">
            <div class="wide posts-container" v-if="posts.length > 0">
              <HfPost v-for="post in posts" :key="post.id" :postInfor="post" :isEdit="false"/>
            </div>
            <HfNoData v-else />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.collection-favorite {
  margin-top: 40px;
}
.basic-collection {
  flex-direction: column;
  align-items: center;
  .collection-background {
    width: 110px;
    height: 110px;
    border-radius: 50%;
    border: 5px solid #2eca7f;
    background-color: #2eca7f;
    overflow: hidden;
    cursor: pointer;
    transition: all 0.3s linear;
    @include flex;
    > img {
      width: 100%;
    }
    &:hover {
      scale: 0.9;
    }
  }
  .collection-name {
    margin: 16px 0 0;
    color: #333;
    font-weight: 500;
  }
  .collection-account {
    @include flex(center, center);
    color: #717171;
    margin-bottom: 18px;
  }
  .btn-container {
    @include mobile {
      button {
        width: 100%;
        margin: 0 0 16px;
      }
    }
  }
}
.active {
  background-color: #000;
  color: #fff;
}
.posts-container{
  margin: 40px 0;
}
</style>
<route lang="yaml">
path: '/detail-collection/:id'
name: DetailCollection
meta:
  layout: default
  requiresAuth: true
</route>
