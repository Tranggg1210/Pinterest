import { apiPHP } from './index';
import { ApiConstant } from '@/constant/api.constant';

const commentApi = () => ({
  getCommentsById: async(id) => apiPHP.get(`${ApiConstant.comment.getCommentByPostId}/${id}`),
  createComment: async({PostId, Content, CommentId  }) => apiPHP.post(ApiConstant.comment.create, {
    PostId,
    Content,
    CommentId 
  }),
  deleteComment: async(commentId) => apiPHP.delete(`${ApiConstant.comment.delete}/${commentId}`),
  updateComment: async({commentId, Content}) => apiPHP.put(ApiConstant.comment.update, {
    commentId,
    Content
  })
});

export const { getCommentsById, createComment, deleteComment, updateComment } = commentApi();
