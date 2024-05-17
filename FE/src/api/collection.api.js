import { ApiConstant } from '@/constant/api.constant';
import { api} from '.';

const collectionApi = () => ({
  getAllCollection: async() => api.get(ApiConstant.collection.collectionAll),
  createCollection: async({name}) => api.post(ApiConstant.collection.createCollection,{name}),
  getCollectionByUserId: async() => api.get(ApiConstant.collection.getCollectionByUserId)
});

export const { 
    getAllCollection,
    createCollection,
    getCollectionByUserId
} =  collectionApi();
