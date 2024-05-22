import { ApiConstant } from '@/constant/api.constant';
import { api, apiUpload} from '.';

const collectionApi = () => ({
  getAllCollection: async() => api.get(ApiConstant.collection.collectionAll),
  createCollection: async({name}) => api.post(ApiConstant.collection.createCollection,{name}),
  getCollectionByUserId: async() => api.get(ApiConstant.collection.getCollectionByUserId),
  getCollectionByPostId: async(id) => api.get(`${ApiConstant.collection.getCollectionByPostId}/${id}`),
  getCollectionById: async(id) => api.get(`${ApiConstant.collection.getById}/${id}`),
  savePostInCollection: async({postId, collectionId}) => {
    const params = new URLSearchParams();
    params.append('postId', postId);
    if (collectionId) {
      params.append('collectionId', collectionId);
    }
    return api.post(`${ApiConstant.collection.savePost}?${params.toString()}`);
  },
  updateBackground: (id, {file}) => apiUpload.post(`${ApiConstant.collection.updateBackgound}/${id}`,{file}),
  updateCollection: async(id, {name, description}) => api.put(`${ApiConstant.collection.updateCollection}/${id}`,{name, description}),
  deleteCollection: async(id) => api.delete(`${ApiConstant.collection.deleteCollection}/${id}`)
});

export const { 
    getAllCollection,
    createCollection,
    getCollectionByUserId,
    getCollectionByPostId,
    getCollectionById,
    savePostInCollection,
    updateBackground,
    updateCollection,
    deleteCollection
} =  collectionApi();
