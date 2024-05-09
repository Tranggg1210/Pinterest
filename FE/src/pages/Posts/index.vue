<script setup>
import { createPost } from '@/api/post.api';
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';

const message = useMessage();
const loadingBar = useLoadingBar();
const disabledRef = ref(true);
const router = useRouter()
const loading = ref(false);
const posts = reactive({
  link: null,
  caption: null,
  detail: null,
  theme: null,
  file: null
});
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
    disabledRef.value = false;
    if (
      data.file.file?.type === 'image/png' ||
      data.file.file?.type === 'image/jpg' ||
      data.file.file?.type === 'image/jpeg' ||
      data.file.file?.type === 'image/gif'
    ) {
      posts.file = data.file;
      loadingBar.finish();
      disabledRef.value = true;
      return true;
    }
    message.error('Vui lòng nhập đúng định dạng ảnh');
    return false;
  } catch (err) {
    console.log(err);
    message.error("Cập nhập ảnh người dùng không thành công!");
  }finally{
    loadingBar.finish();
    disabledRef.value = true;
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
      loading.value = true;
      try {
        loadingBar.start();
        disabledRef.value = false;
        loadingBar.finish();
        await createPost(posts)
        disabledRef.value = true;
        message.success('Tạo bài viết thành công!!!');
        setTimeout(() => {
          router.push('/');
        }, 1000)
      } catch (err) {
        disabledRef.value = true
        loadingBar.error()
        message.error("Tạo bài viết thất bại");
      }
      loading.value = false;
    }
  });
}
</script>

<template>
  <div class="container posts">
    <div class="wide">
      <div class="handle-posts">
        <div class="image-uploaded">
          <n-upload
            multiple
            directory-dnd
            @before-upload="beforeUpload"
            :max="5"
          >
            <n-upload-dragger >
              <div style="margin-bottom: 12px">
                <IconUpload size="40" style="opacity: 0.3" />
              </div>
              <n-text style="font-size: 16px"> Chọn một tệp hoặc kéo thả ở đây </n-text>
              <n-p depth="3" style="margin: 8px 0 0 0">
                Bạn nên sử dụng các ảnh có đuôi là .jpg, .png, .jpeg, .gif
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
                  minRows: 4,
                  maxRows: 6
                }"
              />
            </n-form-item>
            <n-form-item label="Chủ đề" path="theme">
              <n-input v-model:value="posts.theme" placeholder="Thêm chủ đề cho bài viết" class="posts-input" />
            </n-form-item>
            <n-form-item label="Nguồn" path="link">
              <n-input v-model:value="posts.link" placeholder="Thêm nguồn cho bài viết" class="posts-input" />
            </n-form-item>
            <n-form-item class="btn-container">
              <n-button
                attr-type="submit"
                block
                :bordered="false"
                text-color="white"
                icon-placement="left"
                size="large"
                :loading="loading"
                @click="handleCreatePost"
                class="btn-create"
              >
                Đăng
              </n-button>
            </n-form-item>
          </n-form>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped src="./Posts.scss"></style>
<route lang="yaml">
  name: Posts
  meta:
    layout: default
    requiresAuth: true
</route>
