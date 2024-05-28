<script setup>
import { defineProps, ref, reactive, onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';
import { useLoadingBar, useMessage } from 'naive-ui';
import { deletePostById, updatePost } from '@/api/post.api';
import { 
  createCollection, 
  getCollectionByPostId, 
  getCollectionByUserId, 
  isCheckSaveCollection, 
  savePostInCollection 
} from '@/api/collection.api';

const { postInfor, isEdit, isNotDefault } = defineProps(['postInfor', 'isEdit', 'isNotDefault']);
const emit = defineEmits(['updateSavedProducts', 'deletePost', 'updatePosts']);
const router = useRouter();
const message = useMessage();
const loadingBar = useLoadingBar();
const imageURL = ref('');
const showModalEdit = ref(false);
const showModal = ref(false);
const generalOptions = ref([]);
const isCheckSave = ref(false);
const table = ref({ name: null });
const formRef = ref(null);
const formTableRef = ref(null);
const posts = reactive({
  link: postInfor.link,
  caption: postInfor.caption,
  detail: postInfor.detail,
  collectionId: null,
  theme: postInfor.theme,
  file: postInfor.thumbnailUrl
});

// Rules
const rules = {
  caption: {
    required: true,
    validator: (_, caption) => caption?.trim() ? true : new Error('Vui lòng nhập tiêu đề bài viết!'),
    trigger: ['blur', 'input']
  },
};
const rulesTable = {
  name: {
    required: true,
    validator: (_, name) => name?.trim() ? true : new Error('Vui lòng nhập tên bảng!'),
    trigger: ['blur', 'input']
  },
};

// Methods
const handleURLImage = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    imageURL.value = URL.createObjectURL(blob);
  } catch (error) {
    console.error('Lỗi khi tải ảnh:', error);
  }
};

const loadTableByUserId = async () => {
  try {
    const result = await getCollectionByUserId();
    generalOptions.value = result.map(({ name, id }) => ({ label: name, value: id }));
  } catch (error) {
    message.error("Lấy danh sách bảng thất bại!!!");
  }
};

const loadCollectionId = async () => {
  try {
    const result = await getCollectionByPostId(postInfor.id);
    posts.collectionId = result[0]?.id || null;
  } catch (error) {
    message.error("Lấy id collection thất bại!!!");
  }
};

const checkSaveCollection = async () => {
  try {
    isCheckSave.value = await isCheckSaveCollection(postInfor.id);
  } catch (error) {
    message.error("Không thể check các bài viết đã lưu!!!");
  }
};

onBeforeMount(async () => {
  handleURLImage(postInfor.thumbnailUrl);
  await checkSaveCollection();
});

const handleShowModal = async () => {
  showModalEdit.value = true;
  await loadTableByUserId();
  await loadCollectionId();
};

const goToDetailProduct = (id) => router.push(`/detail-post/${id}`);

const beforeUpload = async (data) => {
  try {
    loadingBar.start();
    const { type } = data.file.file;
    if (['image/png', 'image/jpg', 'image/jpeg', 'image/gif', 'image/webp'].includes(type)) {
      posts.file = data.file.file;
      return true;
    }
    message.error('Vui lòng nhập đúng định dạng ảnh');
    return false;
  } catch (error) {
    message.error("Cập nhập ảnh người dùng không thành công!");
  } finally {
    loadingBar.finish();
  }
};

const handleCreateTable = async () => {
  formTableRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        await createCollection({ name: table.value.name });
        message.success("Tạo bảng thành công!!!");
        showModal.value = false;
        await loadTableByUserId();
      } catch (error) {
        message.error('Tạo bảng thất bại!!!');
      } finally {
        loadingBar.finish();
      }
    }
  });
};

const handleUpdatePost = async () => {
  formRef.value?.validate(async (errors) => {
    if (!errors) {
      try {
        loadingBar.start();
        if (posts.file === postInfor.thumbnailUrl) posts.file = null;
        await updatePost(postInfor.id, posts);
        message.success('Cập nhập bài viết thành công!!!');
        emit('updatePosts');
      } catch (error) {
        message.error("Cập nhập bài viết thất bại");
      } finally {
        loadingBar.finish();
      }
    }
  });
};

const handleDeletePost = async () => {
  try {
    loadingBar.start();
    await deletePostById(postInfor.id);
    message.success('Xóa bài viết thành công!!!');
    emit('deletePost', postInfor.id);
  } catch (error) {
    message.error("Xóa bài viết thất bại");
  } finally {
    showModalEdit.value = false;
    loadingBar.finish();
  }
};

const handleSaveCollection = async () => {
  try {
    loadingBar.start();
    await savePostInCollection({ postId: postInfor.id });
    await checkSaveCollection();
    emit('updateSavedPosts', postInfor.id);
    message.success(isCheckSave.value ? 'Lưu bài viết thành công!!!' : 'Hủy lưu bài viết thành công!!!');
  } catch (error) {
    message.error(isCheckSave.value ? "Lưu bài viết thất bại" : "Hủy lưu bài viết thất bại");
  } finally {
    loadingBar.finish();
  }
};
const handleSavePostInCollection = async() => {
  try {
    await savePostInCollection({
      postId: postInfor.id,
      collectionId: isNotDefault
    });
    emit('updateSavedPosts', postInfor.id);
    message.success(`Hủy lưu thành công!!!`)
  } catch (error) {
    console.log(error);
    message.error(`Lỗi không thể hủy lưu`)
  }
}
</script>

<template>
  <div class="post__item">
    <div class="post-image">
      <img :src="postInfor?.thumbnailUrl" alt="image" loading="lazy" />
      <div class="model" @click="() => goToDetailProduct(postInfor?.id)">
        <div class="model__header" v-if="!isEdit">
          <button class="btn-post-save" @click.stop="handleSaveCollection" v-if="!isNotDefault">
            {{ isCheckSave ? "Hủy lưu" : "Lưu" }}
          </button>
          <button class="btn-post-save" @click.stop="handleSavePostInCollection" v-else>
            Hủy lưu
          </button>
        </div>
        <div class="model__footer">
          <IconEdit class="icon" v-show="isEdit" @click.stop="handleShowModal" />
          <n-drawer v-model:show="showModalEdit" style="width: 40%;">
            <n-drawer-content>
              <template #header> Chỉnh sửa bài viết </template>
              <div class="infor-uploaded">
                <n-form ref="formRef" :model="posts" :rules="rules" size="large">
                  <n-form-item label="Ảnh minh họa của bài viết:" path="file">
                    <n-upload @before-upload="beforeUpload" :default-file-list="posts.file ? [{ id: '1', name: 'anh-ban-dau', url: postInfor?.thumbnailUrl }] : []">
                      <n-button>Upload ảnh</n-button>
                    </n-upload>
                  </n-form-item>
                  <n-form-item label="Tiêu đề" path="caption">
                    <n-input v-model:value="posts.caption" placeholder="Thêm tiêu đề" class="posts-input" />
                  </n-form-item>
                  <n-form-item label="Mô tả" path="detail">
                    <n-input v-model:value="posts.detail" placeholder="Thêm mô tả chi tiết" type="textarea" class="posts-input" :autosize="{ minRows: 4, maxRows: 6 }" />
                  </n-form-item>
                  <n-form-item path="collectionId" label="Bảng">
                    <n-select class="posts-input posts-select" v-model:value="posts.collectionId" placeholder="Chọn bảng" :options="generalOptions" />
                    <n-button type="success" class="btn-create-table" @click="showModal = true"> Tạo bảng </n-button>
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
                <n-button @click="handleUpdatePost">Chỉnh sửa</n-button>
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
  <n-modal v-model:show="showModal" class="custom-card" preset="card" title="Thêm bảng" style="width: 60%;" :bordered="false">
    <n-form ref="formTableRef" :model="table" :rules="rulesTable" size="large">
      <n-form-item label="Tên bảng" path="name">
        <n-input v-model:value="table.name" placeholder="Tên bảng" class="posts-input" />
      </n-form-item>
      <n-form-item class="container-end">
        <n-button @click="() => { showModal = false; table.name = '' }"> Hủy </n-button>
        <n-button type="success" style="color: white; margin-left:12px;" @click="handleCreateTable"> Tạo bảng </n-button>
      </n-form-item>
    </n-form>
  </n-modal>
</template>

<style scoped lang="scss" src="./HfPost.scss"></style>
