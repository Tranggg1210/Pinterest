import { ApiConstant } from '@/constant/api.constant';
import { api} from '.';

const collectionApi = () => ({
  getAllCollection: async() => api.get(ApiConstant.collection.collectionAll),
});

export const { 
    getAllCollection
} =  collectionApi();
