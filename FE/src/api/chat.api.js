import { apiPHP } from './index';
import { ApiConstant } from '@/constant/api.constant';

const chatApi = () => ({
  getAllConversation: async () => apiPHP.get(ApiConstant.chat.getConversation),
  createConversation: async (id, { content }) =>
    apiPHP.post(`${ApiConstant.chat.createConversation}/${id}`, {
      content
    }),
  getMessages: async (id) => apiPHP.get(`${ApiConstant.chat.getMessageById}/${id}`),
  sendMessages: async ({ id, content }) =>
    apiPHP.post(ApiConstant.chat.sendMessages, {
      id,
      content
    })
});

export const { getAllConversation, createConversation, getMessages, sendMessages } = chatApi();
