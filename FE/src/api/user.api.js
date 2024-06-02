import { ApiConstant } from '@/constant/api.constant';
import { api, apiUpload } from '.';

const userApi = () => ({
  getAllUser: async () => api.get(ApiConstant.user.getAll),
  getCurrentUser: async () => api.get(ApiConstant.user.currentUser),
  changeAvatar: async ({ file }) => apiUpload.put(ApiConstant.user.changeAvatar, { file }),
  changeInforUser: async ({ firstName, lastName, introduction, birthday, gender, country }) =>
    api.put(ApiConstant.user.changeInfor, {
      firstName,
      lastName,
      introduction,
      birthday,
      gender,
      country
    }),
  changePassword: async ({ oldPassword, newPassword, comfirmPassword }) =>
    api.put(ApiConstant.auth.changePassword, { oldPassword, newPassword, comfirmPassword }),
  deleteUser: async (id) => api.delete(`${ApiConstant.user.deleteAccount}/${id}`),
  getUserById: async (id) => api.get(`${ApiConstant.user.getUserById}/${id}`),
  followerByUserId: async (id) => api.post(`${ApiConstant.user.followUser}/${id}`),
  unFollowerByUserId: async (id) => api.post(`${ApiConstant.user.unFollowUser}/${id}`),
  checkFollowByUserId: async (id) => api.get(`${ApiConstant.user.checkFollow}/${id}`),
  changeInforUserById: async (
    id,
    { firstName, lastName, introduction, birthday, gender, country, avatarUrl }
  ) =>
    apiUpload.put(`${ApiConstant.user.userUpdate}/?id=${id}`, {
      firstName,
      lastName,
      introduction,
      birthday,
      gender,
      country,
      avatarUrl
    })
});

export const {
  getAllUser,
  getCurrentUser,
  changeAvatar,
  changeInforUser,
  changePassword,
  deleteUser,
  getUserById,
  followerByUserId,
  unFollowerByUserId,
  checkFollowByUserId,
  changeInforUserById
} = userApi();
