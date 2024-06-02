import { defineStore } from 'pinia';
import { createComment, deleteComment, getCommentsById, updateComment } from '@/api/comment.api';
import { useMessage } from 'naive-ui';
export const useCommentStore = defineStore({
  id: 'comment',
  state: () => ({
    comments: [],
  }),
  actions: {
    async loadComments(postId) {
      try {
        const result = await getCommentsById(postId);
        this.comments = result.data;
      } catch (error) {
        console.error('Lỗi khi tải danh sách bình luận:', error);
      }
    },
    async createComment({ postId, content }) {
      try {
        await createComment({
          PostId: postId,
          Content: content,
        });
        await this.loadComments(postId);
      } catch (error) {
        console.error('Lỗi khi tạo bình luận:', error);
        message.error("Lỗi không thể tạo bình luận");
      }
    },
    async deleteComment(commentId) {
      try {
        await deleteComment(commentId);
        this.comments = this.comments.filter((comment) => comment.Id !== commentId);
      } catch (error) {
        console.error('Lỗi khi xóa bình luận:', error);
      }
    },
    async updateComment({ commentId, content }) {
      try {
        await updateComment({
          commentId: commentId,
          Content: content,
        });
        this.comments = this.comments.map((comment) => {
          if (comment.Id === commentId) {
            return { ...comment, Content: content };
          }
          return comment;
        });
      } catch (error) {
        console.error('Lỗi khi cập nhật bình luận:', error);
      }
    },
  },
});
