import { api, apiDefault, apiDefaultPHP } from './index';
import { ApiConstant } from '@/constant/api.constant';

const notificationApi = () => ({
   getNotication: async() => api.get(ApiConstant.analysises.getNotication),
   deleteNotification: async(id) => api.delete(`${ApiConstant.analysises.deleteNotication}/${id}`)
});

export const { getNotication, deleteNotification } = notificationApi();
