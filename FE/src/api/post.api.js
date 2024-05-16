import { api, apiUpload } from './index';
import { ApiConstant } from '@/constant/api.constant';

const postApi = () => ({
  getAllPost: async () => api.get(ApiConstant.post.postAll),
  createPost: async({link, caption, detail, theme, file}) => apiUpload.post(ApiConstant.post.createPost,{
    link, 
    caption, 
    detail, 
    theme,
    file
  }),
  getPostById: async(id) => api.get(`${ApiConstant.post.postById}/${id}`),
  getAllPostByUserId: async() => api.get(ApiConstant.post.postByUserId)
});

export const { 
  getAllPost,
  createPost,
  getPostById,
  getAllPostByUserId
 } = postApi();
