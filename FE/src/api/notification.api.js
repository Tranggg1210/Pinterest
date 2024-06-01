import { api, apiDefault, apiDefaultPHP } from './index';
import { ApiConstant } from '@/constant/api.constant';

const notificationApi = () => ({
   getNotication: async() => api.get(ApiConstant.analysises.getNotication)
});

export const { getNotication } = notificationApi();
