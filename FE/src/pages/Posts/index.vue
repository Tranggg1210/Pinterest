<script setup>
import { createCollection, getCollectionByUserId } from '@/api/collection.api';
import { createPost } from '@/api/post.api';
import { viVN } from 'naive-ui';
import { ref, reactive, onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';

const message = useMessage();
const loadingBar = useLoadingBar();
const router = useRouter();
const showModal = ref(false);
const generalOptions = ref([]);
const posts = reactive({
  link: null,
  caption: null,
  detail: null,
  theme: null,
  collectionId: null,
  file: null
});
const table = ref({
  name: null
});
const formRef = ref(null);
const formTableRef = ref(null);
const rules = {
  caption: {
    required: true,
    validator: (_, caption) => {
      if (caption === null || typeof caption === 'undefined') {
        return new Error('Vui lòng nhập tiêu đề bài viết!');
      }

      if (caption.trim() === '') {
        return new Error('Vui lòng nhập tiêu đề bài viết!');
      }
    },
    trigger: ['blur', 'input']
  }
};
const rulesTable = {
  name: {
    required: true,
    validator: (_, name) => {
      if (name === null || typeof name === 'undefined') {
        return new Error('Vui lòng nhập tên bảng!');
      }

      if (name.trim() === '') {
        return new Error('Vui lòng nhập tên bảng!');
      }
    },
    trigger: ['blur', 'input']
  }
};
const loadTableByUserId = async () => {
  try {
    const result = await getCollectionByUserId();
    let data = result?.map((choose) => ({ label: choose.name, value: choose.id }));
    generalOptions.value = data;
  } catch (error) {
    console.log(error);
    message.error('Lấy danh sách bảng thất bại!!!');
  }
};
onBeforeMount(loadTableByUserId);
const beforeUpload = async (data) => {
  try {
    loadingBar.start();
    if (
      data.file.file?.type === 'image/png' ||
      data.file.file?.type === 'image/jpg' ||
      data.file.file?.type === 'image/jpeg' ||
      data.file.file?.type === 'image/gif' ||
      data.file.file?.type === 'image/webp'
    ) {
      posts.file = data.file.file;
      loadingBar.finish();
      return true;
    }
    message.error('Vui lòng nhập đúng định dạng ảnh');
    return false;
  } catch (err) {
    console.log(err);
    message.error('Cập nhập ảnh người dùng không thành công!');
  } finally {
    loadingBar.finish();
  }
};

const handleCreatePost = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      if (!posts.file) {
        message.error('Vui lòng upload ảnh của bài viết');
        return;
      }
      loadingBar.start();
      try {
        await createPost(posts);
        message.success('Tạo bài viết thành công!!!');
        setTimeout(() => {
          router.push('/');
        }, 1000);
      } catch (err) {
        loadingBar.error();
        message.error('Tạo bài viết thất bại');
      }
      loadingBar.finish();
    }
  });
};
const handleCreateTable = async () => {
  formTableRef.value?.validate(async (errors) => {
    if (!errors) {
      loadingBar.start();
      try {
        await createCollection({ name: table.value.name });
        message.success('Tạo bảng thành công!!!');
        showModal.value = false;
        await loadTableByUserId();
      } catch (error) {
        loadingBar.error();
        console.log(error);
        message.error('Tạo bảng thất bại!!!');
      }
      loadingBar.finish();
    }
  });
};
</script>

<template>
  <div class="container posts">
    <div class="wide">
      <h1 class="title">Tạo bài viết</h1>
      <div class="handle-posts">
        <div class="image-uploaded">
          <n-upload directory-dnd @before-upload="beforeUpload">
            <n-upload-dragger class="upload-dragger">
              <div style="margin-bottom: 12px">
                <IconUpload size="40" style="opacity: 0.3" />
              </div>
              <n-text style="font-size: 16px"> Chọn một tệp hoặc kéo thả ở đây </n-text>
              <n-p depth="3" style="margin: 8px 0 0 0">
                Bạn nên sử dụng các ảnh có đuôi là .jpg, .png, .jpeg, .gif, .webp
              </n-p>
            </n-upload-dragger>
          </n-upload>
        </div>
        <div class="infor-uploaded">
          <n-form ref="formRef" :model="posts" :rules="rules" size="large">
            <n-form-item label="Tiêu đề" path="caption">
              <n-input
                v-model:value="posts.caption"
                placeholder="Thêm tiêu đề"
                class="posts-input"
              />
            </n-form-item>
            <n-form-item label="Mô tả" path="detail">
              <n-input
                v-model:value="posts.detail"
                placeholder="Thêm mô tả chi tiết"
                type="textarea"
                class="posts-input"
                :autosize="{
                  minRows: 2,
                  maxRows: 4
                }"
              />
            </n-form-item>
            <n-form-item path="collectionId" label="Bảng">
              <n-select
                class="posts-input posts-select"
                :bordered="false"
                v-model:value="posts.collectionId"
                placeholder="Chọn bảng"
                :options="generalOptions"
              />
              <n-button type="success" class="btn-create-table" @click="showModal = true">
                Tạo bảng
              </n-button>
            </n-form-item>
            <n-form-item label="Hashtab" path="theme">
              <n-input
                v-model:value="posts.theme"
                placeholder="Thêm hashtab cho bài viết"
                class="posts-input"
              />
            </n-form-item>
            <n-form-item label="Nguồn" path="link">
              <n-input
                v-model:value="posts.link"
                placeholder="Thêm nguồn cho bài viết"
                class="posts-input"
              />
            </n-form-item>
            <n-form-item class="btn-container">
              <HfButton type="submit" @click="handleCreatePost"> Đăng </HfButton>
            </n-form-item>
          </n-form>
        </div>
      </div>
    </div>
  </div>
  <n-modal
    v-model:show="showModal"
    class="custom-card"
    preset="card"
    title="Modal"
    style="width: 60%"
    :bordered="false"
  >
    <n-form ref="formTableRef" :model="table" :rules="rulesTable" size="large">
      <n-form-item label="Tên bảng" path="name">
        <n-input v-model:value="table.name" placeholder="Tên bảng" class="posts-input" />
      </n-form-item>
      <n-form-item class="container-end">
        <n-button
          @click="
            () => {
              showModal = false;
              table.name = '';
            }
          "
        >
          Hủy
        </n-button>
        <n-button type="success" style="color: white; margin-left: 12px" @click="handleCreateTable">
          Tạo bảng
        </n-button>
      </n-form-item>
    </n-form>
  </n-modal>
</template>

<style lang="scss" scoped src="./Posts.scss"></style>
<route lang="yaml">
name: Posts
meta:
  layout: default
  requiresAuth: true
</route>
