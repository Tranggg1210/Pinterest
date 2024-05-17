<script setup>
import { getAllCollection } from '@/api/collection.api';
import { getAllPost, getPostById } from '@/api/post.api';
import { checkFollowByUserId, followerByUserId, getUserById, unFollowerByUserId } from '@/api/user.api';
import { useLoadingBar, useMessage } from 'naive-ui';
import { useRouter } from 'vue-router';
const router = useRouter();
const imageURL = ref('');
const message = useMessage();
const post = ref({});
const postList = ref([]);
const loadingBar = useLoadingBar();

const loadPost = async() => {
  try {
    const result = await getPostById(router.currentRoute.value.params.id);
    post.value = result;
    if(post?.value.userId)
    {
      const user = await getUserById(post.value.userId);
      post.value.user = user;
      const checkFollow = await checkFollowByUserId(post.value.userId);
      post.value.isFollowUser = checkFollow;
    }
  } catch (error) {
    console.log(error);
    message.error("Lỗi không tải được dữ liệu của bài viết, vui lòng thử lại sau");
  }
}

const loadAllCollection = async() => {
  try {
    const data = await getAllCollection();
    postList.value = data;
  } catch (error) {
    console.log(error);
    message.error("Lỗi không tải được danh sách gợi ý, vui lòng thử lại sau");
  }
}

const handleURLImage = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    imageURL.value = URL.createObjectURL(blob);
  } catch (error) {
    console.error('Lỗi khi tải ảnh:', error);
  }
};
onBeforeMount(async() => {
  await loadPost();
  handleURLImage(post?.value.thumbnailUrl);
  await loadAllCollection();
});
const goBack = () => {
  router.back();
}

const handleFollowUser = async() => {
  loadingBar.start();
  try {
    if(post?.value.userId)
    {
      await followerByUserId(post.value.userId);
    }
    await loadPost();
    loadingBar.finish();
  } catch (error) {
    loadingBar.error();
    console.log(error);
    message.error("Lỗi, không thể theo dõi người dùng này, vui lòng thử lại sau");
  }
}

const handleUnFollowUser = async() => {
  loadingBar.start();
  try {
    if(post?.value.userId)
    {
      await unFollowerByUserId(post.value.userId);
    }
    await loadPost();
    loadingBar.finish();
  } catch (error) {
    loadingBar.error();
    console.log(error);
    message.error("Lỗi, không thể hủy theo dõi người dùng này, vui lòng thử lại sau");
  }
}
</script>
<template>
  <div class="container">
    <div class="wide">
      <div class="detail-post">
        <div @click="goBack">
          <IconArrowLeft class="icon icon-back" />
        </div>
        <div class="detail-post-container">
          <div class="detail-post-left">
            <img :src="post.thumbnailUrl" alt="" v-if="post && post.thumbnailUrl" />
            <img src="@/assets/images/no-data.jpg" v-else alt="" />
          </div>
          <div class="detail-post-right">
            <div class="detail-right-option">
              <div class="option-left">
                <a download :href="imageURL" title="ImageName">
                   <IconDownload class="icon"></IconDownload>
                </a>
              </div>
              <div class="option-right">
                <div class="collection-name">
                  <button class="btn-post-save">Lưu</button>
                </div>
              </div>
            </div>

            <div class="detail-right-title">{{ post?.caption ? post.caption : "Bài viết chưa có tiêu đề" }}</div>
            <div class="detail-right-des">
              {{ post?.detail && post.detail }}
            </div>
            <div class="detail-right-hashtab">
              {{ post?.theme && post.theme }}
            </div>
            <div class="detail-right-link">
               {{ post?.link && `👉Nguồn tham khảo: ${ post.link} 👈` }}
            </div>
            <div class="detail-right-user">
              <div class="user-avatar">
                <img :src="post.user.avatarUrl" alt="" v-if="post && post.user && post.user.avatarUrl" />
                <img src="@/assets/images/user-avatar.png" v-else alt="" />
                <p class="user-name">
                  {{ (post && post.user && post.user.userName) ? post.user.userName : "Không xác định"}}
                </p>
              </div>
              <HfButton class="btn-follow" @click="handleFollowUser" v-if="!post.isFollowUser">Theo dõi</HfButton>
              <HfButton class="btn-follow" @click="handleUnFollowUser" v-if="post.isFollowUser">Hủy theo dõi</HfButton>
            </div>

            <div class="detail-right-comment">
              <div class="title">Nhận xét</div>
              <IconChevronUp class="icon" size="24"></IconChevronUp>
            </div>
            <div class="user-comment"></div>
          </div>
        </div>
      </div>
      <div class="relate-posts">
        <h3>Thêm nội dung để khám phá</h3>
        <div class="posts-container" v-if="postList.length > 0" >
          <HfPost v-for="post in postList" :key="post.id" :postInfor="post"/>
        </div>
        <div v-else>
          <HfNoData />
        </div>
      </div>
      <br><br>
    </div>
  </div>
</template>

<style scoped lang="scss" src="./DetailPost.scss"></style>

<route lang="yaml">
path: '/detail-post/:id'
name: DetailPost
meta:
  layout: default
  requiresAuth: true
</route>
