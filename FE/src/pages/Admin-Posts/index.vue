<script setup>
import { getAllPost } from '@/api/post.api';
import { NButton, useMessage } from 'naive-ui';
import { h, onBeforeMount } from 'vue';

const posts = ref([]);
const loading = ref(false);
const columns = [
  {
    title: 'ID',
    key: 'id',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.id - b.id,
    }
  },
  {
    title: 'Ảnh minh họa',
    key: 'thumbnailUrl',
    align: 'center',
    render(row) {
      return h('img', {
        src: row.thumbnailUrl,
        style: {
          width: '70px',
          height: '70px',
          cursor: 'pointer'
        },
        onClick: () => console.log(row)
      });
    }
  },
  {
    title: 'Tiêu đề',
    key: 'caption'
  },
  {
    title: 'Mô tả',
    key: 'detail',
  },
  {
    title: 'Lượt thích',
    key: 'like',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.like - b.like,
    }
  },
  {
    title: 'Chủ đề',
    key: 'theme',
  },
  {
    title: 'Action',
    key: 'actions',
    render (row) {
      return h(
        NButton,
        {
          strong: true,
          tertiary: true,
          size: 'small',
        },
        { default: () => 'Play' }
      )
    }
  }
]

const pagination = ref({ pageSize: 5 });
const message = useMessage();
const showModal = ref(false);
const postValue = reactive({
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
  }
};
const loadPosts = async() => {
  try {
    loading.value = true;
    const result = await getAllPost();
    posts.value = result;
    loading.value = false;
    message.success("Tải danh sách bài viết thành công");
  } catch (error) {
    loading.value = false;
    message.error('Tải danh sách bài viết thất bại')
  }
}
onBeforeMount(loadPosts);
const beforeUpload = async (data) => {
  try {
    loadingBar.start();
    const { type } = data.file.file;
    if (['image/png', 'image/jpg', 'image/jpeg', 'image/gif', 'image/webp'].includes(type)) {
      postValue.file = data.file.file;
      return true;
    }
    message.error('Vui lòng nhập đúng định dạng ảnh');
    return false;
  } catch (error) {
    message.error('Cập nhập ảnh người dùng không thành công!');
  } finally {
    loadingBar.finish();
  }
};
const handleCreatePost = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      if (!postValue.file) {
        message.error('Vui lòng upload ảnh của bài viết');
        return;
      }
      loadingBar.start();
      try {
        await createPost(postValue);
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
</script>
<template>
  <div class="admin-posts">
    <div class="admin-header">
      <h2>Danh sách bài viết</h2>
      <n-button type="warning" @click="showModal = true">
        Thêm bài viết
      </n-button>
    </div>
     <HfLoading v-if="loading"></HfLoading>
    <div v-else>
      <n-data-table
        class="data-table"
        ref="dataTableInst"
        :columns="columns"
        :data="toRaw(posts)"
        :pagination="pagination"
      />
    </div>
    <n-modal
      v-model:show="showModal"
      class="custom-card"
      preset="card"
      title="Thêm bài viết"
      style="width: 60%"
      :bordered="false"
    >
      <n-form ref="formRef" :model="postValue" :rules="rules" size="large">
        <n-form-item label="Ảnh minh họa của bài viết:" path="file">
          <n-upload
            @before-upload="beforeUpload"
          >
            <n-button>Upload ảnh</n-button>
          </n-upload>
        </n-form-item>
        <n-form-item label="Tiêu đề" path="caption">
          <n-input
            v-model:value="postValue.caption"
            placeholder="Thêm tiêu đề"
            class="input"
          />
        </n-form-item>
        <n-form-item label="Mô tả" path="detail">
          <n-input
            v-model:value="postValue.detail"
            placeholder="Thêm mô tả chi tiết"
            type="textarea"
            class="input"
            :autosize="{
              minRows: 2,
              maxRows: 4
            }"
          />
        </n-form-item>
        <n-form-item label="Hashtab" path="theme">
          <n-input
            v-model:value="postValue.theme"
            placeholder="Thêm hashtab cho bài viết"
            class="input"
          />
        </n-form-item>
        <n-form-item label="Nguồn" path="link">
          <n-input
            v-model:value="postValue.link"
            placeholder="Thêm nguồn cho bài viết"
            class="input"
          />
        </n-form-item>
        <n-form-item class="container-end">
          <n-button
            @click="() => {
              showModal = false;
              postValue.link = '',
              postValue.caption = '',
              postValue.detail = '',
              postValue.theme = '',
              postValue.file = ''
            }"
          >
            Hủy
          </n-button>
          <n-button type="success" style="color: white; margin-left: 12px" @click="handleCreatePost">
            Tạo bài viết
          </n-button>
        </n-form-item>
      </n-form>
    </n-modal>
  </div>
</template>


<style lang="scss" scoped>
.admin-posts
{
  overflow-x: scroll;
  padding: 40px;
}
.admin-header{
  @include flex(space-between, center);
  margin: 20px 0;
  h2{
    font-weight: 500;
    text-decoration: underline;
  }
}
.admin-posts::-webkit-scrollbar {
  height: 8px; 
  display: none;
}
.admin-posts::-webkit-scrollbar-thumb {
  background-color: #ccc; 
  border-radius: 4px; 
}
.admin-posts::-webkit-scrollbar-track {
  background-color: #f1f1f1;
}
</style>
<route lang="yaml">
name: Admin-Posts
meta:
  layout: admin
  requiresAuth: true
</route>
