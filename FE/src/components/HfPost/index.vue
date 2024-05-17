<script setup>
import { defineProps, onBeforeMount, ref } from 'vue';
import router from '@/router';
import { deletePostById } from '@/api/post.api';
const { postInfor, isEdit } = defineProps(['postInfor', 'isEdit']);
const imageURL = ref('');
const showModalEdit = ref(false);
const message = useMessage();
const loadingBar = useLoadingBar();
const posts = reactive({
  link: postInfor.link,
  caption: postInfor.caption,
  detail: postInfor.detail,
  theme: postInfor.theme,
  file: postInfor.thumbnailUrl
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

const handleURLImage = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    imageURL.value = URL.createObjectURL(blob);
  } catch (error) {
    console.error('Lỗi khi tải ảnh:', error);
  }
};
onBeforeMount(() => handleURLImage(postInfor?.thumbnailUrl));
const handleShowModal = () => {
  showModalEdit.value = true
}
const goToDetailProduct = (id) => {
  router.push(`/detail-post/${id}`)
}

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

const handleUpdatePost = async() => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      if(!posts.file)
      {
        message.error("Vui lòng upload ảnh của bài viết");
        return;
      }
      loadingBar.start();
      try {
        message.success('Tạo bài viết thành công!!!');
      } catch (err) {
        loadingBar.error()
        message.error("Tạo bài viết thất bại");
      }
      loadingBar.finish();
    }
  });
}
const handleDeletePost = async() => {
  loadingBar.start();
  try {
    await deletePostById(postInfor.id);
    showModalEdit.value = false;
    window.location.reload();
    message.success('Xóa bài viết thành công!!!');
  } catch (error) {
    console.log(error);
    loadingBar.error(); 
    showModalEdit.value = false;
    message.error("Xóa bài viết thất bại");
  }
  loadingBar.finish();
}
</script>

<template>
  <div class="post__item">
    <div class="post-image">
      <img :src="postInfor?.thumbnailUrl" alt="image" loading="lazy" />
      <div class="model" @click="() => goToDetailProduct(postInfor?.id)">
        <div class="model__header">
          <button class="btn-post-save">Lưu</button>
        </div>
        <div class="model__footer">
          <IconEdit class="icon" v-show="isEdit" @click.stop="handleShowModal"/>
          <n-drawer v-model:show="showModalEdit" :width="502">
            <n-drawer-content>
              <template #header>
                Chỉnh sửa bài viết
              </template>
              <div class="infor-uploaded">
                <n-form
                  ref="formRef"
                  :model="posts"
                  :rules="rules"
                  size="large"
                >
                <n-form-item label="Ảnh minh họa của bài viết:" path="file">
                  <n-upload 
                    @before-upload="beforeUpload"
                    :default-file-list="posts.file ? [{
                      id: '1',
                      name: 'anh',
                      url: posts.file
                    }] : []"
                  >
                    <n-button>Upload ảnh</n-button>
                  </n-upload>
                </n-form-item>
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
                  <n-form-item label="Hashtab" path="theme">
                    <n-input v-model:value="posts.theme" placeholder="Thêm hashtab cho bài viết" class="posts-input" />
                  </n-form-item>
                  <n-form-item label="Nguồn" path="link">
                    <n-input v-model:value="posts.link" placeholder="Thêm nguồn cho bài viết" class="posts-input" />
                  </n-form-item>
                </n-form>
              </div>
              <template #footer>
                <n-button @click="handleDeletePost" style="margin-right: 8px;">Xóa bài viết</n-button>
                <n-button  @click="handleUpdatePost">Chỉnh sửa</n-button>
              </template>
            </n-drawer-content>
          </n-drawer>
          <a download :href="imageURL" title="ImageName" @click.stop>
            <IconDownload class="icon" size="12"></IconDownload>
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.icon {
  cursor: pointer;
}
.post__item {
  z-index: 1;
  min-height: 100px;
  .post-image {
    position: relative;
    height: 100%;
    width: 100%;

    img {
      width: 100%;
      height: 100%;
      object-fit: cover;
      border-radius: 25px;
    }

    &:hover .model {
      visibility: visible;
      background-color: rgba(0, 0, 0, 0.4);
      cursor: pointer;
    }
    .model {
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      z-index: 100;
      visibility: hidden;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      border-radius: 25px;
    }
  }
}
img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}
.model__header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  color: white;
  font-weight: 700;
  font-size: 15px;
  padding: 10px 15px;
  .colection-name {
    display: flex;
    justify-content: center;
    .icon {
      margin-left: 5px;
    }
  }

  .btn-post-save {
    background-color: red;
    color: white;
    font-weight: 700;
    font-size: 15px;
    border-radius: 20px;
    padding: 14px 18px;
    border: none;
    cursor: pointer;
    position: relative;
    z-index: 118;
  }
}

.model__footer {
  position: absolute;
  float: right;
  right: 10px;
  bottom: 5px;
  .icon {
    padding: 5px 5px;
    color: #000;
    width: 32px;
    height: 32px;
    background-color: #f7f7f7;
    border-radius: 100%;
    position: relative;
    z-index: 118;
  }
}
.posts-input{
  border: 1px solid #ddd;
}
</style>
