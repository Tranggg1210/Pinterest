import { api } from './index';
import { ApiConstant } from '@/constant/api.constant';

const postApi = () => ({
  getAllPost: async() => api.get(ApiConstant.post.postAll)
});

export const { getAllPost } = postApi();
