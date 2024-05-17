<script setup>
import { createPost } from '@/api/post.api';
import { viVN } from 'naive-ui';
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';

const message = useMessage();
const loadingBar = useLoadingBar();
const router = useRouter();
const showModal = ref(false);
const generalOptions = ref([
    { label: 'Chưa có bảng nào được tạo', value: '' },
])
const posts = reactive({
  link: null,
  caption: null,
  detail: null,
  table: null,
  theme: null,
  file: null
});
const table = ref({
  name: null,
  title: null
})
const formRef = ref(null);
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
  },
};
const beforeUpload = async(data) => {
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
    message.error("Cập nhập ảnh người dùng không thành công!");
  }finally{
    loadingBar.finish();
  }
};

const handleCreatePost = async() => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      if(!posts.file)
      {
        message.error("Vui lòng upload ảnh của bài viết");
        return;
      }
      loadingBar.start();
      try {
        const result = await createPost(posts);
        console.log(posts);
        console.log(result)
        message.success('Tạo bài viết thành công!!!');
        setTimeout(() => {
          router.push('/');
        }, 1000)
      } catch (err) {
        loadingBar.error()
        message.error("Tạo bài viết thất bại");
      }
      loadingBar.finish();
    }
  });
}
</script>

<template>
  <div class="container posts">
    <div class="wide">
      <h1 class="title">Tạo bài viết</h1>
      <div class="handle-posts">
        <div class="image-uploaded">
          <n-upload
            directory-dnd
            @before-upload="beforeUpload"
          >
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
          <n-form
            ref="formRef"
            :model="posts"
            :rules="rules"
            size="large"
          >
            <n-form-item label="Tiêu đề" path="caption">
              <n-input v-model:value="posts.caption"  placeholder="Thêm tiêu đề" class="posts-input" />
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
            <n-form-item path="table" label="Bảng">
                <n-select
                    class="posts-input"
                    :bordered="false"
                    v-model:value="posts.table"
                    placeholder="Chọn bảng"
                    :options="generalOptions"
                />
                <n-button type="success" class="btn-create-table" @click="showModal = true">
                  Tạo bảng
                </n-button>
            </n-form-item>
            <n-form-item label="Hashtab" path="theme">
              <n-input v-model:value="posts.theme" placeholder="Thêm hashtab cho bài viết" class="posts-input" />
            </n-form-item>
            <n-form-item label="Nguồn" path="link">
              <n-input v-model:value="posts.link" placeholder="Thêm nguồn cho bài viết" class="posts-input" />
            </n-form-item>
            <n-form-item class="btn-container">
              <HfButton
                type="submit"
                @click="handleCreatePost"
              >
                Đăng
              </HfButton>
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
    :segmented="segmented"
  >
    Content
    <template #footer>
      Footer
    </template>
  </n-modal>
</template>

<style lang="scss" scoped src="./Posts.scss"></style>
<route lang="yaml">
  name: Posts
  meta:
    layout: default
    requiresAuth: true
</route>
