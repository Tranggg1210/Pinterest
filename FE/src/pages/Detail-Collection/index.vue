<script setup>
import { deleteCollection, getCollectionById, updateBackground, updateCollection } from '@/api/collection.api';
import { getPostByCollectionId } from '@/api/post.api';
import { useDialog, useLoadingBar, useMessage } from 'naive-ui';
import { onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';

const message = useMessage();
const loading = ref(false);
const router = useRouter();
const collectionInfor = ref(null);
const loadingBar = useLoadingBar()
const posts = ref([]);
const showModal = ref(false);
const dialog = useDialog();
const collectionForm = ref({
  file: null,
  name: null,
  description: null
});
const formRef = ref(null);
const rulesCollection = {
  name: {
    required: true,
    validator: (_, name) => {
      if (name === null || typeof name === 'undefined') {
        return new Error('Vui lòng nhập tên bộ sưu tập!');
      }

      if (name.trim() === '') {
        return new Error('Vui lòng nhập tên bộ sưu tập!');
      }
    },
    trigger: ['blur', 'input']
  },
}
const options = [
    {
      label: "Cập nhập bộ sưu tập",
      key: 1
    },
    {
      label: "Xóa bộ sưu tập",
      key: 2
    }
];

const loadCollection = async() => {
  try {
    const result = await getCollectionById(router.currentRoute.value.params?.id);
    collectionInfor.value = result;
    collectionForm.value = {
      file: collectionInfor.value.backgroundUrl,
      name: collectionInfor.value.name,
      description: collectionInfor.value.description
    }
  } catch (error) {
    console.log(error);
    message.error("Lỗi không thể tải được dữ liệu của collection");
  }
}
const loadPostOfCollection = async() => {
  try {
    loading.value = true;
    if(collectionInfor.value)
    {
      if(collectionInfor.value.isDefault)
      {
        const result = await getPostByCollectionId();
        posts.value = result;
      }else{
        const result = await getPostByCollectionId(collectionInfor?.value.id);
        posts.value = result;
        console.log(result);
      }
    }
    loading.value = false;
  } catch (error) {
    console.log(error);
    loading.value = false;
    message.error("Lỗi không thể tải được danh sách bài viết của collection");
  }
}
onBeforeMount(async() => {
  await loadCollection();
  if(collectionInfor.value)
  {
    await loadPostOfCollection();
  }
});
const removePost = (postId) => {
  posts.value = posts.value.filter(post => post.id !== postId);
};
const handleName = (name) => {
  const formattedName = name
    .replace(/\s+/g, ' ')
    .trim()
    .replace(/(^|\s)\S/g, (match) => match.toUpperCase());

  return formattedName;
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
      collectionForm.value.file = data.file.file;
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
const handleUpdateCollection = async() =>{
  try {
    loadingBar.start();
    await updateCollection(collectionInfor.value.id, {
      name: collectionForm.value.name,
      description: collectionForm.value.description
    });
    if(collectionForm.value.file !== collectionInfor.value.backgroundUrl)
    {
      await updateBackground(collectionInfor.value.id, {
        file: collectionForm.value.file
      })
    }
    showModal.value = false;
    await loadCollection();
    message.success("Cập nhập bộ sưu tập thành công");
  } catch (error) {
    console.log(error);
    loadingBar.error();
    message.error("Lỗi không thể cập nhập bộ sưu tập ");
  }
  loadingBar.finish();
}
const handleDeleteCollection = () => {
  dialog.warning({
    title: 'Xóa bộ sưu tập',
    content: 'Bạn có chắc chắn muốn xóa bộ sưu tập này này?',
    positiveText: 'Hủy',
    negativeText: 'Xóa bộ sưu tập',
    onNegativeClick: async () => {
      try {
        loadingBar.start();
        await deleteCollection(collectionInfor.value.id);
        message.success('Xóa bộ sưu tập thành công!');
        router.push('/profile-favorite');
      } catch (err) {
        console.log(err);
        message.error("Xóa bộ sưu tập không thành công!")
      }
      loadingBar.finish();
    },
    onPositiveClick: () => {

    }
  });
};
const handleSelectDropdown = (key) => {
  if(key === 1)
  {
    showModal.value = true;
  }else{
    handleDeleteCollection();
  }
}
const gotoPage = () => {
  router.push('/profile-favorite')
}
</script>
<template>
  <div>
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
            <n-button @click="gotoPage">Quay lại trang trước</n-button>
            <n-dropdown trigger="click" :options="options" @select="handleSelectDropdown" v-if="!collectionInfor?.isDefault">
              <n-button>Chỉnh sửa bộ sưu tập</n-button>
            </n-dropdown>
            <n-button v-else @click=" showModal = true;">Chỉnh sửa bộ sưu tập</n-button>
          </n-space>
          <div>
            <HfLoading v-if="loading"/>
            <div v-else class="container">
              <div class="wide posts-container" v-if="posts.length > 0">
                <HfPost v-for="post in posts" :key="post.id" :postInfor="post" :isEdit="false" @updateSavedPosts="removePost"/>
              </div>
              <HfNoData v-else />
            </div>
          </div>
        </div>
      </div>
    </div>
    <n-modal
      v-model:show="showModal"
      class="custom-card"
      preset="card"
      title="Cập nhập bộ sưu tập"
      style="width: 60%;"
      :bordered="false"
    >
    <n-form
      ref="formRef"
      :model="collectionForm"
      :rules="rulesCollection"
      size="large"
    >
    <n-form-item label="Tên bộ sưu tập:" path="name" v-if="!collectionInfor?.isDefault">
      <n-input v-model:value="collectionForm.name"  placeholder="Tên bộ sưu tập" class="posts-input" />
    </n-form-item>
    <n-form-item label="Tên bộ sưu tập:" path="name" v-else>
      <n-input v-model:value="collectionForm.name" disabled  placeholder="Tên bộ sưu tập" class="posts-input" />
    </n-form-item>
    <n-form-item label="Mô tả bộ sưu tập:" path="description">
      <n-input v-model:value="collectionForm.description"  placeholder="Mô tả bộ sưu tập" class="posts-input" />
    </n-form-item>
    <n-form-item label="Ảnh minh họa của bài viết:" path="file">
      <n-upload 
        @before-upload="beforeUpload">
        <n-button>Upload ảnh</n-button>
      </n-upload>
    </n-form-item>
    <n-form-item class="container-end">
        <n-button type="success" style="color: white; margin-left:12px ;" @click="handleUpdateCollection">
          Cập nhập bộ sưu tập
        </n-button>
      </n-form-item>
    </n-form>
    </n-modal>
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
.posts-input{
  border: 1px solid #ccc;
}
.container-end{
  display: flex;
  justify-content: end;
}
</style>
<route lang="yaml">
path: '/detail-collection/:id'
name: DetailCollection
meta:
  layout: default
  requiresAuth: true
</route>
