import { ApiConstant } from '@/constant/api.constant';
import { api} from '.';

const collectionApi = () => ({
  getAllCollection: async() => api.get(ApiConstant.collection.collectionAll),
  createCollection: async({name}) => api.post(ApiConstant.collection.createCollection,{name}),
  getCollectionByUserId: async() => api.get(ApiConstant.collection.getCollectionByUserId),
  getCollectionByPostId: async(id) => api.get(`${ApiConstant.collection.getCollectionByPostId}/${id}`),
  savePostInCollection: async({postId, collectionId}) => {
    const params = new URLSearchParams();
    params.append('postId', postId);
    if (collectionId) {
      params.append('collectionId', collectionId);
    }
    return api.post(`${ApiConstant.collection.savePost}?${params.toString()}`);
  }
});

export const { 
    getAllCollection,
    createCollection,
    getCollectionByUserId,
    getCollectionByPostId,
    savePostInCollection
} =  collectionApi();
