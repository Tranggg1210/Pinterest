import { ApiConstant } from '@/constant/api.constant';
import { api, apiUpload } from '.';

const userApi = () => ({
  getCurrentUser: async () => api.get(ApiConstant.user.currentUser),
  changeAvatar: async ({file}) => apiUpload.put(ApiConstant.user.changeAvatar, {file}),
  changeInforUser: async ({ firstName, lastName, introduction, birthday, gender, country }) =>
    api.put(ApiConstant.user.changeInfor, { firstName, lastName, introduction, birthday, gender, country }),
  changePassword: async({oldPassword, newPassword, comfirmPassword}) => 
    api.put(ApiConstant.auth.changePassword, {oldPassword, newPassword, comfirmPassword}),
  deleteUser: async(id) => api.delete(`${ApiConstant.user.deleteAccount}/${id}`),
  getUserById: async(id) => api.get(`${ApiConstant.user.getUserById}/${id}`),
  followerByUserId: async(id) => api.post(`${ApiConstant.user.followUser}/${id}`)
});

export const { 
  getCurrentUser, 
  changeAvatar,
  changeInforUser, 
  changePassword, 
  deleteUser, 
  getUserById,
  followerByUserId
} = userApi();
