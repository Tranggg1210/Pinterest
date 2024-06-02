<script setup>
import { createPost, deletePostById, getAllPost, updatePost } from '@/api/post.api';
import { NButton, useDialog, useLoadingBar, useMessage } from 'naive-ui';
import { h, onBeforeMount } from 'vue';

const posts = ref([]);
const loading = ref(false);
const loadingBar = useLoadingBar();
const pagination = ref({ pageSize: 4 });
const message = useMessage();
const showModal = ref(false);
const dialog = useDialog();
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

const loadPosts = async () => {
  try {
    const result = await getAllPost();
    posts.value = result;
  } catch (error) {
    console.log(error);
  }
};
onBeforeMount(async () => {
  try {
    loading.value = true;
    await loadPosts();
    loading.value = false;
    message.success('Tải danh sách bài viết thành công');
  } catch (error) {
    loading.value = false;
    message.error('Tải danh sách bài viết thất bại');
  }
});
const handleDeletePost = async (id) => {
  try {
    loadingBar.start();
    await deletePostById(id);
    message.success('Xóa bài viết thành công!!!');
    await loadPosts();
  } catch (error) {
    message.error('Xóa bài viết thất bại');
  } finally {
    loadingBar.finish();
  }
};
const oldDataBeforeUpdate = ref({});
const isEdit = ref(false);

const columns = [
  {
    title: 'ID',
    key: 'id',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.id - b.id
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
    key: 'detail'
  },
  {
    title: 'Lượt thích',
    key: 'like',
    defaultSortOrder: false,
    sorter: {
      compare: (a, b) => a.like - b.like
    }
  },
  {
    title: 'Chủ đề',
    key: 'theme'
  },
  {
    title: 'Actions',
    key: 'actions',
    render(row) {
      const buttons = [
        {
          label: 'Chỉnh sửa',
          type: 'info',
          onClick: () => {
            oldDataBeforeUpdate.value = row;
            isEdit.value = true;
            postValue.link = row.link;
            postValue.caption = row.caption;
            postValue.detail = row.detail;
            postValue.theme = row.theme;
            postValue.file = row.thumbnailUrl;
            showModal.value = true;
          }
        },
        {
          label: 'Xóa',
          type: 'primary',
          onClick: () => {
            dialog.warning({
              title: 'Xóa bài viết',
              content: 'Bạn có chắc chắn muốn xóa bài viết này?',
              positiveText: 'Hủy',
              negativeText: 'Xóa bài viết',
              onNegativeClick: () => {
                handleDeletePost(row.id);
              },
              onPositiveClick: () => {}
            });
          }
        }
      ];

      return h(
        'div',
        {
          style: {
            display: 'flex',
            gap: '8px'
          }
        },
        buttons.map((btn) =>
          h(
            NButton,
            {
              strong: true,
              tertiary: true,
              size: 'small',
              type: btn.type,
              onClick: btn.onClick
            },
            { default: () => btn.label }
          )
        )
      );
    }
  }
];

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
    message.error('Lỗi tải ảnh lên không thành công!');
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
        await loadPosts();
        showModal.value = false;
      } catch (err) {
        loadingBar.error();
        message.error('Tạo bài viết thất bại');
      }
      loadingBar.finish();
    }
  });
};
const handleUpdatePost = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        if (posts.file === oldDataBeforeUpdate.value.thumbnailUrl) postValue.file = null;
        await updatePost(oldDataBeforeUpdate.value.id, postValue);
        message.success('Cập nhập bài viết thành công!!!');
        await loadPosts();
        showModal.value = false;
      } catch (error) {
        message.error('Cập nhập bài viết thất bại');
      } finally {
        loadingBar.finish();
      }
    }
  });
};
</script>
<template>
  <div class="admin-posts">
    <div class="admin-header">
      <h2>Danh sách bài viết</h2>
      <n-button type="warning" @click="showModal = true"> Thêm bài viết </n-button>
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
      :title="isEdit ? 'Chỉnh sửa bài viết' : 'Thêm bài viết'"
      style="width: 60%"
      :bordered="false"
    >
      <n-form ref="formRef" :model="postValue" :rules="rules" size="large">
        <n-form-item label="Ảnh minh họa của bài viết:" path="file">
          <n-upload @before-upload="beforeUpload">
            <n-button>Upload ảnh</n-button>
          </n-upload>
        </n-form-item>
        <n-form-item label="Tiêu đề" path="caption">
          <n-input v-model:value="postValue.caption" placeholder="Thêm tiêu đề" class="input" />
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
            @click="
              () => {
                showModal = false;
              }
            "
          >
            Hủy
          </n-button>
          <n-button
            type="success"
            style="color: white; margin-left: 12px"
            @click="handleCreatePost"
            v-if="!isEdit"
          >
            Tạo bài viết
          </n-button>
          <n-button
            type="success"
            style="color: white; margin-left: 12px"
            @click="handleUpdatePost"
            v-else
          >
            Chỉnh sửa bài viết
          </n-button>
        </n-form-item>
      </n-form>
    </n-modal>
  </div>
</template>

<style lang="scss" scoped>
.admin-posts {
  overflow-x: scroll;
  padding: 20px 40px 40px;
}
.admin-header {
  @include flex(space-between, center);
  margin: 20px 0;
  h2 {
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
