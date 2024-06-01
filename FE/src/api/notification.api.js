import { api, apiDefault, apiDefaultPHP } from './index';
import { ApiConstant } from '@/constant/api.constant';

const notificationApi = () => ({
   getNotication: async() => api.get(ApiConstant.analysises.getNotification),
   deleteNotification: async(id) => api.delete(`${ApiConstant.analysises.deleteNotification}/${id}`),
   createNotification: async({data}) => api.post(ApiConstant.analysises.createNotification, {data})
});

export const { getNotication, deleteNotification,createNotification } = notificationApi();
