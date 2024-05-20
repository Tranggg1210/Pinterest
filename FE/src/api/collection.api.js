import { ApiConstant } from '@/constant/api.constant';
import { api} from '.';

const collectionApi = () => ({
  getAllCollection: async() => api.get(ApiConstant.collection.collectionAll),
  createCollection: async({name}) => api.post(ApiConstant.collection.createCollection,{name}),
  getCollectionByUserId: async() => api.get(ApiConstant.collection.getCollectionByUserId),
  getCollectionByPostId: async(id) => api.get(`${ApiConstant.collection.getCollectionByPostId}/${id}`)
});

export const { 
    getAllCollection,
    createCollection,
    getCollectionByUserId,
    getCollectionByPostId
} =  collectionApi();
