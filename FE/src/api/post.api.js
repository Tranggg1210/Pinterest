import { api } from './index';
import { ApiConstant } from '@/constant/api.constant';

const postApi = () => ({
  getAllPost: async() => api.get(ApiConstant.post.postAll),
  // createPost: async({link, caption}) => api.post(ApiConstant.post.createPost, {})
});

export const { getAllPost } = postApi();
