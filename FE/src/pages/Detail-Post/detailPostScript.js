import { ref, onBeforeMount } from 'vue';
import {
  createCollection,
  getAllCollection,
  getCollectionByUserId,
  isCheckSaveCollection,
  savePostInCollection
} from '@/api/collection.api';
import { checkLike, getPostById, toggleLike } from '@/api/post.api';
import {
  checkFollowByUserId,
  followerByUserId,
  getUserById,
  unFollowerByUserId
} from '@/api/user.api';
import { useLoadingBar, useMessage } from 'naive-ui';
import { useRouter } from 'vue-router';
import { useCommentStore } from '@/stores/commentStore';
const commentStore = useCommentStore();

export const useDetailPost = () => {
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
  const options = ref([]);
  const like = ref(false);
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

  const fetchData = async (apiFunc, successCallback) => {
    try {
      const result = await apiFunc();
      successCallback(result);
    } catch (error) {
      console.error(error);
    }
  };

  const loadTableByUserId = () => {
    fetchData(getCollectionByUserId, (result) => {
      if (result.length > 0) {
        const data = result.filter((item) => item.isDefault === false);
        options.value = data.map((choose) => ({ label: choose.name, key: choose.id }));
      }
      options.value.push({
        label: '📌 Tạo bảng',
        key: 'create'
      });
    });
  };

  const loadPost = () => {
    loading.value = true;
    fetchData(
      async () => {
        const result = await getPostById(router.currentRoute.value.params.id);
        post.value = result;
        if (post.value.userId) {
          const user = await getUserById(post.value.userId);
          post.value.user = user;
          post.value.isFollowUser = await checkFollowByUserId(post.value.userId);
          loading.value = false;
        }
      },
      () => {}
    );
  };

  const checkSaveCollection = () => {
    fetchData(
      () => isCheckSaveCollection(router.currentRoute.value.params.id),
      (result) => {
        isCheckSave.value = result;
      }
    );
  };

  const loadAllCollection = () => {
    fetchData(getAllCollection, (data) => {
      postList.value = data;
    });
  };

  const handleURLImage = async (url) => {
    try {
      const response = await fetch(url);
      const blob = await response.blob();
      const imageUrl = URL.createObjectURL(blob);
      imageURL.value = imageUrl;
    } catch (error) {
      console.error('Lỗi khi tải ảnh:', error);
    }
  };

  const loadCheckLike = async () => {
    try {
      like.value = await checkLike(router.currentRoute.value.params.id);
    } catch (error) {
      console.log(error);
    }
  };

  onBeforeMount(async () => {
    try {
      await loadPost();
      await checkSaveCollection();
      await loadAllCollection();
      await loadTableByUserId();
      await loadCheckLike();
      handleURLImage(post?.value.thumbnailUrl);
      await commentStore.loadComments(router.currentRoute.value.params.id);
    } catch (error) {
      message.error('Tải dữ liệu thất bại');
      router.back();
    }
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
      message.error('Lỗi, vui lòng thử lại sau');
    }
  };

  const handleFollowUser = () => handleFollowUnfollowUser(followerByUserId);
  const handleUnFollowUser = () => handleFollowUnfollowUser(unFollowerByUserId);

  const handleSaveCollection = async () => {
    loadingBar.start();
    try {
      await savePostInCollection({ postId: router.currentRoute.value.params.id });
      await checkSaveCollection();
      message.success(
        isCheckSave.value
          ? 'Lưu bài viết vào bảng mặc định thành công!!!'
          : 'Hủy lưu bài viết vào bảng mặc định thành công!!!'
      );
    } catch (error) {
      console.error(error);
      loadingBar.error();
      message.error(
        isCheckSave.value
          ? 'Lưu bài viết vào bảng mặc định thất bại'
          : 'Hủy lưu bài viết vào bảng mặc định thất bại'
      );
    }
    loadingBar.finish();
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
  const handleSavePostInCollection = async (key, label) => {
    try {
      await savePostInCollection({
        postId: post?.value.id,
        collectionId: key
      });
      message.success(`Lưu vào ${label.label} thành công!!!`);
    } catch (error) {
      console.log(error);
      message.error(`Lỗi không thể lưu vào ${label.label}`);
    }
  };
  const handleSelect = (key, label) => {
    if (key === 'create') {
      showModal.value = true;
    } else {
      handleSavePostInCollection(key, label);
    }
  };
  const gotoPage = () => {
    router.push(`/user-articles/${post.value.user.id}`);
  };
  const handleLike = async () => {
    try {
      await toggleLike(post?.value.id);
      fetchData(
        async () => {
          const result = await getPostById(router.currentRoute.value.params.id);
          post.value = result;
          if (post.value.userId) {
            const user = await getUserById(post.value.userId);
            post.value.user = user;
            post.value.isFollowUser = await checkFollowByUserId(post.value.userId);
          }
        },
        () => {},
        'Lỗi không tải được dữ liệu của bài viết, vui lòng thử lại sau'
      );
      await loadCheckLike();
    } catch (error) {
      console.log(error);
      message.error(`Lỗi không thể like bài viết`);
    }
  };

  return {
    imageURL,
    message,
    post,
    postList,
    loadingBar,
    showModal,
    isCheckSave,
    table,
    formTableRef,
    loading,
    options,
    like,
    rulesTable,
    goBack,
    handleFollowUser,
    handleUnFollowUser,
    handleSaveCollection,
    handleCreateTable,
    handleSelect,
    gotoPage,
    handleLike
  };
};
