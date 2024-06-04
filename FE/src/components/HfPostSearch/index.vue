<script setup>
import { defineProps, ref, reactive, onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';
import { useLoadingBar, useMessage } from 'naive-ui';
import { isCheckSaveCollection, savePostInCollection } from '@/api/collection.api';

const { postInfor, isNotDefault } = defineProps(['postInfor', 'isNotDefault']);
const emit = defineEmits(['updateSavedProducts', 'deletePost', 'updatePosts']);
const router = useRouter();
const message = useMessage();
const loadingBar = useLoadingBar();
const imageURL = ref('');
const isCheckSave = ref(false);

const handleURLImage = async (url) => {
  try {
    const response = await fetch(url);
    const blob = await response.blob();
    imageURL.value = URL.createObjectURL(blob);
  } catch (error) {
    console.error('Lỗi khi tải ảnh:', error);
  }
};

const checkSaveCollection = async () => {
  try {
    isCheckSave.value = await isCheckSaveCollection(postInfor.Id);
  } catch (error) {
    message.error('Không thể check các bài viết đã lưu!!!');
  }
};

onBeforeMount(async () => {
  handleURLImage(postInfor.ThumbnailUrl);
  await checkSaveCollection();
});

const goToDetailProduct = (id) => router.push(`/detail-post/${id}`);

const handleSaveCollection = async () => {
  try {
    loadingBar.start();
    await savePostInCollection({ postId: postInfor.Id });
    await checkSaveCollection();
    emit('updateSavedPosts', postInfor.Id);
    message.success(
      isCheckSave.value ? 'Lưu bài viết thành công!!!' : 'Hủy lưu bài viết thành công!!!'
    );
  } catch (error) {
    message.error(isCheckSave.value ? 'Lưu bài viết thất bại' : 'Hủy lưu bài viết thất bại');
  } finally {
    loadingBar.finish();
  }
};
const handleSavePostInCollection = async () => {
  try {
    await savePostInCollection({
      postId: postInfor.Id,
      collectionId: isNotDefault
    });
    emit('updateSavedPosts', postInfor.Id);
    message.success(`Hủy lưu thành công!!!`);
  } catch (error) {
    console.log(error);
    message.error(`Lỗi không thể hủy lưu`);
  }
};
</script>

<template>
  <div class="post__item">
    <div class="post-image">
      <img :src="postInfor?.ThumbnailUrl" alt="image" loading="lazy" />
      <div class="model" @click="() => goToDetailProduct(postInfor?.Id)">
        <div class="model__header">
          <button class="btn-post-save" @click.stop="handleSaveCollection" v-if="!isNotDefault">
            {{ isCheckSave ? 'Hủy lưu' : 'Lưu' }}
          </button>
          <button class="btn-post-save" @click.stop="handleSavePostInCollection" v-else>
            Hủy lưu
          </button>
        </div>
        <div class="model__footer">
          <a download :href="imageURL" title="ImageName" @click.stop>
            <IconDownload class="icon" size="12"></IconDownload>
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss" src="./HfPost.scss"></style>
