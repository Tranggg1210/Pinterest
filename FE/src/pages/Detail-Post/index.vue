<script setup>
import { ref, onBeforeMount } from 'vue';
import { createCollection, getAllCollection, getCollectionByUserId, isCheckSaveCollection, savePostInCollection } from '@/api/collection.api';
import { checkLike, getPostById, toggleLike } from '@/api/post.api';
import { checkFollowByUserId, followerByUserId, getUserById, unFollowerByUserId } from '@/api/user.api';
import { useLoadingBar, useMessage } from 'naive-ui';
import { useRouter } from 'vue-router';
const router = useRouter();
const imageURL = ref('');
const message = useMessage();
const post = ref({});
const postList = ref([]);
const loadingBar = useLoadingBar();
const showModal = ref(false);
const isCheckSave = ref(false);
const table = ref({ name: null });
const formTableRef = ref(null);
const loading = ref(false);
const options = ref ([])
const like = ref(false);
const rulesTable = {
  name: {
    required: true,
    validator: (_, name) => {
      if (!name?.trim()) {
        return new Error('Vui lòng nhập tên bảng!');
      }
    },
    trigger: ['blur', 'input']
  }
};

const fetchData = async (apiFunc, successCallback, errorMessage) => {
  try {
    const result = await apiFunc();
    successCallback(result);
  } catch (error) {
    console.error(error);
    message.error(errorMessage);
  }
};

const loadTableByUserId = () => {
  fetchData(getCollectionByUserId, (result) => {
    if(result.length > 0)
    {
      const data = result.filter(item => item.isDefault === false);
      options.value = data.map(choose => ({ label: choose.name, key: choose.id }));
    }
    options.value.push({
      label: "📌 Tạo bảng", 
      key: "create"
    })
  }, "Lấy danh sách bảng thất bại!!!");
};

const loadPost = () => {
  loading.value = true;
  fetchData(async () => {
    const result = await getPostById(router.currentRoute.value.params.id);
    post.value = result;
    if (post.value.userId) {
      const user = await getUserById(post.value.userId);
      post.value.user = user;
      post.value.isFollowUser = await checkFollowByUserId(post.value.userId);
      loading.value = false;
    }
  }, () => {}, "Lỗi không tải được dữ liệu của bài viết, vui lòng thử lại sau");
};

const checkSaveCollection = () => {
  fetchData(() => isCheckSaveCollection(router.currentRoute.value.params.id), (result) => {
    isCheckSave.value = result;
  }, "Không thể check các bài viết đã lưu!!!");
};

const loadAllCollection = () => {
  fetchData(getAllCollection, (data) => {
    postList.value = data;
  }, "Lỗi không tải được danh sách gợi ý, vui lòng thử lại sau");
};

const handleURLImage = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    imageURL.value = URL.createObjectURL(blob);
  } catch (error) {
    console.error('Lỗi khi tải ảnh:', error);
  }
};
const loadCheckLike = async() => {
  try {
    like.value = await checkLike(router.currentRoute.value.params.id);
  } catch (error) {
    console.log(error);
  }
}

onBeforeMount(async() => {
  await loadPost();
  handleURLImage(post?.value.thumbnailUrl);
  await checkSaveCollection();
  await loadAllCollection();
  await loadTableByUserId();
  await loadCheckLike();
});

const goBack = () => {
  router.back();
};

const handleFollowUnfollowUser = async (action) => {
  loadingBar.start();
  try {
    if (post.value.userId) {
      await action(post.value.userId);
    }
    await loadPost();
    loadingBar.finish();
  } catch (error) {
    console.error(error);
    loadingBar.error();
    message.error("Lỗi, vui lòng thử lại sau");
  }
};

const handleFollowUser = () => handleFollowUnfollowUser(followerByUserId);
const handleUnFollowUser = () => handleFollowUnfollowUser(unFollowerByUserId);

const handleSaveCollection = async () => {
  loadingBar.start();
  try {
    await savePostInCollection({ postId: router.currentRoute.value.params.id });
    await checkSaveCollection();
    message.success(isCheckSave.value ? 'Lưu bài viết vào bảng mặc định thành công!!!' : 'Hủy lưu bài viết vào bảng mặc định thành công!!!');
  } catch (error) {
    console.error(error);
    loadingBar.error();
    message.error(isCheckSave.value ? 'Lưu bài viết vào bảng mặc định thất bại' : 'Hủy lưu bài viết vào bảng mặc định thất bại');
  }
  loadingBar.finish();
};
const handleCreateTable = async() => {
  formTableRef.value?.validate(async (errors) => {
    if (!errors) {
      loadingBar.start();
      try {
        await createCollection({name: table.value.name});
        message.success("Tạo bảng thành công!!!");
        showModal.value = false;
        await loadTableByUserId();
      } catch (error) {
        loadingBar.error()
        console.log(error);
        message.error('Tạo bảng thất bại!!!');
      }
      loadingBar.finish();
    }
  });
}
const handleSavePostInCollection = async(key, label) => {
  try {
    await savePostInCollection({
      postId: post?.value.id,
      collectionId: key
    });
    message.success(`Lưu vào ${label.label} thành công!!!`)
  } catch (error) {
    console.log(error);
    message.error(`Lỗi không thể lưu vào ${label.label}`)
  }
}
const handleSelect = (key, label) => {
  if(key === 'create')
  {
    showModal.value = true;
  }else{
    handleSavePostInCollection(key, label);
  }
}
const gotoPage = () => {
  router.push(`/user-articles/${post.value.user.id}`)
}
const handleLike = async() => {
  try {
   await toggleLike(post?.value.id);
   fetchData(async () => {
    const result = await getPostById(router.currentRoute.value.params.id);
    post.value = result;
    if (post.value.userId) {
      const user = await getUserById(post.value.userId);
      post.value.user = user;
      post.value.isFollowUser = await checkFollowByUserId(post.value.userId);
    }
  }, () => {}, "Lỗi không tải được dữ liệu của bài viết, vui lòng thử lại sau");
   await loadCheckLike();
  } catch (error) {
    console.log(error);
    message.error(`Lỗi không thể like bài viết`)
  }
}
</script>

<template>
  <div class="container">
   <HfLoading v-if="loading"/>
   <div class="wide" v-else>
      <div class="detail-post">
        <div @click="goBack">
          <IconArrowLeft class="icon icon-back" />
        </div>
        <div class="detail-post-container">
          <div class="detail-post-left">
            <img :src="post.thumbnailUrl" alt="" v-if="post?.thumbnailUrl" />
            <img src="@/assets/images/no-data.jpg" v-else alt="" />
          </div>
          <div class="detail-post-right">
            <div class="detail-right-header">
              <div class="option-left">
                <a download :href="imageURL" title="ImageName">
                   <IconDownload class="icon icon-download"></IconDownload>
                </a>
              </div>
              <div class="option-right">
                <n-dropdown
                  :options="options"
                  trigger="click"
                  @select="handleSelect"
                >
                  <p class="option-right-text">
                    Hồ sơ
                    <IconChevronDown size="24" class="icon-down"></IconChevronDown>
                  </p>
                </n-dropdown>
                <button class="btn-post-save" @click="handleSaveCollection">
                  {{ isCheckSave ? "Hủy lưu" : "Lưu" }}
                </button>
              </div>
            </div>
            <div class="detail-right-body">
              <div class="detail-right-title">{{ post?.caption || "Bài viết chưa có tiêu đề" }}</div>
              <div class="detail-right-des">{{ post?.detail }}</div>
              <div class="detail-right-hashtab">{{ post?.theme }}</div>
              <div class="detail-right-link" v-if="post?.link">👉Nguồn tham khảo: {{ post.link }} 👈</div>
              <div class="detail-right-user" @click="gotoPage">
                <div class="user-avatar">
                  <img :src="post.user?.avatarUrl"  alt="user-avatar" v-if="post.user?.avatarUrl" />
                  <img src="@/assets/images/user-avatar.png" alt="user-avatar" v-else>
                  <div>
                    <p class="user-name">{{ post.user?.userName || "Không xác định" }}</p>
                    <p class="user-follower">{{ post.user?.follower || "0" }} người theo dõi</p>
                  </div>
                </div>
                <HfButton class="btn-follow" @click="handleFollowUser" v-if="!post.isFollowUser">Theo dõi</HfButton>
                <HfButton class="btn-follow" @click="handleUnFollowUser" v-if="post.isFollowUser">Hủy theo dõi</HfButton>
              </div>
              <div class="detail-right-comment">
                <div class="title">Nhận xét</div>
                <IconChevronUp class="icon-comment" size="24"></IconChevronUp>
              </div>
            </div>
            <div class="detail-right-footer">
              <div class="dt-footer-top">
                <p class="quality-comment">
                  0 nhận xét
                </p>
                <div class="dt-like">
                  <div class="quality-like" v-if="post.like > 0">
                    <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                      <g clip-path="url(#clip0)">
                      <path d="M22.18 3.35C24.61 5.82 24.61 9.84 22.18 12.31L12 22.5L1.82 12.31C0.61 11.07 0 9.45 0 7.83C0 6.21 0.61 4.59 1.82 3.35C4.26 0.89 8.22 0.89 10.65 3.35L12 4.72L13.34 3.35C14.56 2.12 16.16 1.5 17.76 1.5C19.36 1.5 20.96 2.12 22.18 3.35Z" fill="#FF5246"/>
                      <path d="M16.24 12.88C16.24 12.9 16.25 12.92 16.25 12.94C16.25 15.25 14.35 17.13 12 17.13C9.65 17.13 7.75 15.26 7.75 12.94C7.75 12.92 7.76 12.9 7.76 12.88C9.02 13.55 10.47 13.93 12 13.93C13.53 13.93 14.98 13.54 16.24 12.88ZM7 7.13C5.96 7.13 5.12 7.97 5.12 9.01C5.12 10.05 5.96 10.88 7 10.88C8.04 10.88 8.87 10.04 8.87 9C8.87 7.96 8.04 7.13 7 7.13ZM17 7.13C15.96 7.13 15.12 7.97 15.12 9.01C15.12 10.05 15.96 10.88 17 10.88C18.04 10.88 18.88 10.04 18.88 9C18.88 7.96 18.04 7.13 17 7.13Z" fill="#720906"/>
                      </g>
                      <defs>
                      <clipPath id="clip0">
                      <rect width="24" height="21" fill="white" transform="translate(0 1.5)"/>
                      </clipPath>
                      </defs>
                    </svg>
                    {{ post.like }}
                  </div>
                  <div :class="!like ? 'toogle-like' : 'liked'" @click="handleLike">
                    <svg v-if="!like" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                      <path d="M5.93992 6C6.85992 6 7.76992 6.37 8.42992 7.02L9.90992 8.46L11.9999 10.5L14.0899 8.46L15.5699 7.02C16.2399 6.37 17.1399 6 18.0599 6C18.5499 6 19.2599 6.11 19.9099 6.63C20.5699 7.16 20.9599 7.9 20.9999 8.71C21.0399 9.52 20.7299 10.28 20.1399 10.86L20.0699 10.93L19.9999 11.01C19.9399 11.08 14.4099 17.23 11.9999 19.76C9.58992 17.22 4.05992 11.07 3.99992 11.01L3.93992 10.93L3.85992 10.86C3.26992 10.28 2.96992 9.52 2.99992 8.71C3.03992 7.9 3.42992 7.16 4.08992 6.63C4.72992 6.11 5.44992 6 5.93992 6ZM18.0599 3C16.3999 3 14.7299 3.65 13.4799 4.87C13.1099 5.23 11.9999 6.31 11.9999 6.31C11.9999 6.31 10.8899 5.23 10.5199 4.87C9.26992 3.65 7.59992 3 5.93992 3C4.60992 3 3.28992 3.42 2.20992 4.29C-0.580081 6.54 -0.730081 10.57 1.76992 13.01C1.76992 13.01 8.05992 20.02 10.2499 22.27C10.7199 22.76 11.3599 23 11.9999 23C12.6399 23 13.2799 22.76 13.7499 22.27C15.9399 20.02 22.2299 13.01 22.2299 13.01C24.7299 10.58 24.5799 6.54 21.7899 4.29C20.7099 3.42 19.3899 3 18.0599 3Z" fill="#111111"/>
                    </svg>
                    <svg v-else width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                      <g clip-path="url(#clip0)">
                      <path d="M22.18 3.35C24.61 5.82 24.61 9.84 22.18 12.31L12 22.5L1.82 12.31C0.61 11.07 0 9.45 0 7.83C0 6.21 0.61 4.59 1.82 3.35C4.26 0.89 8.22 0.89 10.65 3.35L12 4.72L13.34 3.35C14.56 2.12 16.16 1.5 17.76 1.5C19.36 1.5 20.96 2.12 22.18 3.35Z" fill="#FF5246"/>
                      <path d="M16.24 12.88C16.24 12.9 16.25 12.92 16.25 12.94C16.25 15.25 14.35 17.13 12 17.13C9.65 17.13 7.75 15.26 7.75 12.94C7.75 12.92 7.76 12.9 7.76 12.88C9.02 13.55 10.47 13.93 12 13.93C13.53 13.93 14.98 13.54 16.24 12.88ZM7 7.13C5.96 7.13 5.12 7.97 5.12 9.01C5.12 10.05 5.96 10.88 7 10.88C8.04 10.88 8.87 10.04 8.87 9C8.87 7.96 8.04 7.13 7 7.13ZM17 7.13C15.96 7.13 15.12 7.97 15.12 9.01C15.12 10.05 15.96 10.88 17 10.88C18.04 10.88 18.88 10.04 18.88 9C18.88 7.96 18.04 7.13 17 7.13Z" fill="#720906"/>
                      </g>
                      <defs>
                      <clipPath id="clip0">
                      <rect width="24" height="21" fill="white" transform="translate(0 1.5)"/>
                      </clipPath>
                      </defs>
                    </svg>
                  </div>
                </div>
              </div>
              <div class="dt-footer-bottom"></div>
            </div>
          </div>
        </div>
      </div>
      <div class="relate-posts" v-if="postList.length">
        <div class="posts-container">
          <HfPost v-for="post in postList" :key="post.id" :postInfor="post" />
        </div>
      </div>
      <div v-else>
        <HfNoData />
      </div>
    </div>
    <n-modal v-model:show="showModal" class="custom-card" preset="card" title="Tạo bảng" style="width: 40%" :bordered="false">
      <n-form ref="formTableRef" :model="table" :rules="rulesTable" size="large">
        <n-form-item label="Tên bảng" path="name">
          <n-input v-model:value="table.name" placeholder="Tên bảng" class="posts-input" />
        </n-form-item>
        <n-form-item class="container-end">
          <n-button @click="() => { showModal = false; table.name = '' }">Hủy</n-button>
          <n-button type="success" style="color: white; margin-left:12px;" @click="handleCreateTable">Tạo bảng</n-button>
        </n-form-item>
      </n-form>
    </n-modal>
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
