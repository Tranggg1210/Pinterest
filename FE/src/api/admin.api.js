import { ApiConstant } from '@/constant/api.constant';
import { api } from '.';

const adminApi = () => ({
  checkAdmin: async (username) => api.get(`${ApiConstant.admin.checkAdmin}/${username}`),
  getAnalysisToday: async () => api.get(`${ApiConstant.analysises.getAnalysises}`)
});

export const { checkAdmin, getAnalysisToday } = adminApi();
