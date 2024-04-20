<?php

namespace App\Http\Controllers\Api;

use App\Events\CommentPublished;
use App\Http\Controllers\Controller;
use App\Models\Comment;
use App\Models\Post;
use Illuminate\Http\Request;

class CommentController extends Controller
{
    public function sendcomment(Request $request){
        $user = auth() -> user();
        if($request -> PostId == null){
            return response()->json([
               'status' => 403 ,
               'message' => 'Không tồn tại bài viết này',
            ]);
        }
        $post = Post::where('Id', $request -> PostId)-> get();
        if($post){
            $comment = new Comment();
            $comment -> UserId = $user -> Id;
            $comment -> CreatedAt = date_format(date_create(),"Y-m-d H:i:s");
            $comment -> LikeAmount = 0;
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
            event(new CommentPublished($comment));
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
                'status' => 200,
                'data_comment' => $listComments
            ]);
        }else{
            return response()->json([
               'status' => 404,
               'data_comment' => false,
               'message' => 'Không tìm thấy bài viết này'
            ]);
        }
    }
}
