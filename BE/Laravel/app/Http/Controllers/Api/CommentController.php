<?php

namespace App\Http\Controllers\Api;

use App\Events\CommentPublished;
use App\Http\Controllers\Controller;
use App\Models\Comment;
use App\Models\Post;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

/**
 * @OA\Post(
 *     path="/api/comments/{PostId}",
 *     operationId="Comment",
 *     tags={"Comment"},
 *     summary="Comment",
 *     description="Comment id bài viết.Nếu reply lại comment người khác thì điền CommentId cần rep lại",
*    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="PostId",type="interger"),
 *                      @OA\Property(property="CommentId",type="string"),
 *                      @OA\Property(property="Content",type="string"),
 *                      example={"PostId":"Id bài post","Content": "test","CommentId": ""}
 *                  )
 *              }
 *         )
 *     ),
 *     @OA\Response(response=200,description="Comment successfully"),
 *     @OA\Response(response=400,description="Không thể comment"),
 *     @OA\Response(response=404,description="Không tồn tại bài viết"),
 *     @OA\Response(response=502,description="Không tồn tại bình luận đã phản hồi trong bài viết này"),
 * ),
  * @OA\Delete(
 *     path="/api/del-comment/{commentId?}",
 *     tags={"Comment"},
 *     summary="Xoá comment",
 *     description="Xoá cmt theo Id",
*    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="commentId",type="interger"),
 *                      example={"commentId":"Id bài post"}
 *                  )
 *              }
 *         )
 *     ),
 *     @OA\Response(response=200,description="Deleted successfully"),
 *     @OA\Response(response=403,description="Không có quyền xoá"),
 *     @OA\Response(response=404,description="Không tồn tại comment"),
 * ),
  * @OA\Put(
 *     path="/api/edit-comment/{commentId?}",
 *     tags={"Comment"},
 *     summary="Sửa comment",
 *     description="Sửa cmt theo Id",
*    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="commentId",type="interger"),
 *                      example={"commentId":"Id bài post"}
 *                  )
 *              }
 *         )
 *     ),
 *     @OA\Response(response=200,description="Modified successfully"),
 *     @OA\Response(response=403,description="Không có quyền sửa"),
 *     @OA\Response(response=404,description="Không tồn tại comment"),
 * ),
 * @OA\Post(
 *     path="/api/get-comment/{PostId}",
 *     tags={"Comment"},
 *     summary="Lấy toàn bộ comment của bài viết",
 *     description="Lấy toàn bộ comment của bài viết",
 *     @OA\Parameter(
 *         name="PostId",
 *         in="path",
 *         description="ID của bài post",
 *         required=true,
 *         @OA\Schema(
 *             type="integer",
 *             format="int64"
 *         )
 *     ),
 *     @OA\Response(response=200,description="Successfully"),
 *     @OA\Response(response=404,description="Không tồn tại bài viết"),
 * )
 */
class CommentController extends Controller
{
    public function sendcomment(Request $request){
        // $user = auth() -> user();
        $user = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> first();
        if($request -> PostId == null){
            return response()->json([
               'status' => 403 ,
               'message' => 'Không tồn tại bài viết này',
            ]);
        }
        $post = Post::where('Id', $request -> PostId)-> get();
        if(count($post)){
            $comment = new Comment();
            $comment -> UserId = $user -> Id;
            $comment -> CreatedAt = date_format(date_create(),"Y-m-d H:i:s");
            // $comment -> LikeAmount = 0;
            $comment -> PostId = $request -> PostId;
            $comment -> Content = $request -> Content;
            if($request -> CommentId){
                $check = Comment::where('Id', $request -> CommentId)-> where('PostId', $request -> PostId) -> get();
                if(count($check)){
                    $comment -> CommentReplyId = $request -> CommentId;
                }else{
                    return response() -> json([
                        'status' => 502,
                        'message' => "Không tồn tại bình luận đã phản hồi trong bài viết này"
                    ]);
                }
            }
            $comment -> save();
            // $content = $request -> content;
            //
            event(new CommentPublished($comment)); // Tạo event khi comment thành công
            return response()->json([
                'status' => 200,
                'message' => 'Bình luận thành công',
                'comment_id' => $comment -> Id,
                'content' => $comment -> Content,
            ]);
        }else{
            return response()->json([
               'status' => 404 ,
               'message' => 'Bài viết không tồn tại'
            ]);
        }
    }
    public function getComments(Request $request){
        $comment = new Comment();
        $listComments = $comment -> where('PostId',$request -> PostId) -> orderBy('CreatedAt','desc') -> get();
        // $listComments = $comment->orderBy('CreatedAt', 'desc')->take(10)->get();;
        if(count($listComments)){
            return response()->json([
                'status' => 200 ,
                'data' => $listComments
            ]);
        }else{
            return response()->json([
               'status' => 404,
               'data_comment' => false,
               'message' => 'Không tìm thấy bài viết này'
            ]);
        }
    }
    public function editComment(Request $request){
        $validator = Validator::make($request->all(), [
            'commentId' => 'required',
            'Content' => 'required'
        ]);
        if($validator -> fails()){
            return response()->json([
                'error' => $validator->errors()
            ]);
        }else{
            $user = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> first();
            $comment = Comment::where('Id',$request -> commentId) -> first();
            if($comment){
                if($comment -> UserId == $user -> Id){
                    $comment -> Content = $request -> Content;
                    $comment -> save();
                    return response()->json([
                    'status' => 200,
                    'message' => 'Sửa bình luận thành công',
                    'content_modified' => $comment-> Content
                    ]);
                }else{
                    return response()->json([
                    'status' => 403,
                    'message' => 'Bạn không có quyền sửa bình luận này'
                    ]);
                }
            }else{
                return response()->json([
                'status' => 404,
                'message' => 'Bình luận không tồn tại'
                ]);
            }
        }
    }
    public function deleteComment(Request $request){
        $user = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> first();
        $comment = Comment::where('Id',$request -> commentId) -> first();
        if($comment){
            if($comment -> UserId == $user-> Id){
                $comment -> delete();
                return response()->json([
                   'status' => 200,
                   'message' => 'Xóa bình luận thành công'
                ]);
            }else{
                return response()->json([
                   'status' => 403,
                   'message' => 'Bạn không có quyền xóa bình luận này'
                ]);
            }
        }else{
            return response()->json([
               'status' => 404,
               'message' => 'Bình luận không tồn tại'
            ]);
        }
    }
}
