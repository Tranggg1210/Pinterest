import { api, apiPHP, apiUpload } from './index';
import { ApiConstant } from '@/constant/api.constant';

const postApi = () => ({
  getAllPost: async () => api.get(ApiConstant.post.postAll),
  createPost: async ({ link, caption, detail, theme, collectionId, file }) =>
    apiUpload.post(ApiConstant.post.createPost, {
      link,
      caption,
      detail,
      theme,
      collectionId,
      file
    }),
  getPostById: async (id) => api.get(`${ApiConstant.post.postById}/${id}`),
  getAllPostByUserId: async (id) => {
    if (id) {
      return api.get(`${ApiConstant.post.postByUserId}?userId=${id}`);
    }
    return api.get(ApiConstant.post.postByUserId);
  },
  deletePostById: async (id) => api.delete(`${ApiConstant.post.deletePostById}/${id}`),
  updatePost: async (id, { link, caption, detail, theme, file, collectionId }) =>
    apiUpload.put(`${ApiConstant.post.updatePostById}/${id}`, {
      link,
      caption,
      detail,
      theme,
      file,
      collectionId
    }),
  getPostByCollectionId: async (id) => {
    if (!id) {
      return api.get(`${ApiConstant.post.getByCollectionId}`);
    }
    return api.get(`${ApiConstant.post.getByCollectionId}?collectionId=${id}`);
  },
  toggleLike: async (id) => api.post(`${ApiConstant.post.toggleLike}?postId=${id}`),
  checkLike: async (id) => api.get(`${ApiConstant.post.checkLike}/${id}`),
  searchPosts: async(keyword) => apiPHP.get(`${ApiConstant.post.search}/${keyword}`)
});

export const {
  getAllPost,
  createPost,
  getPostById,
  getAllPostByUserId,
  deletePostById,
  updatePost,
  getPostByCollectionId,
  toggleLike,
  checkLike,
  searchPosts
} = postApi();
