<script setup>
import { ref, onBeforeMount } from 'vue';
import { createCollection, getAllCollection, getCollectionByUserId, isCheckSaveCollection, savePostInCollection } from '@/api/collection.api';
import { getPostById } from '@/api/post.api';
import { checkFollowByUserId, followerByUserId, getUserById, unFollowerByUserId } from '@/api/user.api';
import { useLoadingBar, useMessage } from 'naive-ui';
import { useRouter } from 'vue-router';
import { NIcon } from 'naive-ui'
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
    console.log(result);
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

onBeforeMount(async() => {
  await loadPost();
  handleURLImage(post?.value.thumbnailUrl);
  await checkSaveCollection();
  await loadAllCollection();
  await loadTableByUserId();
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
                <p class="quality-comment">0 nhận xét</p>
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
