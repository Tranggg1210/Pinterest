<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Post;
use App\Models\User;
use Illuminate\Http\Request;

class SearchController extends Controller
{
    public function search($keyword = null){
        // $keyword = $request -> keyword;
        if(isset($keyword)){
            $user = User::where('first_name','LIKE', '%'. $keyword . '%') -> orWhere('last_name','LIKE', '%'. $keyword . '%') -> get();
            $posts = Post::where('caption', 'LIKE', '%'.$keyword.'%')->get();
            return response()->json([
                'status' => 'success',
                'data' => [
                    'user' => $user,
                    'posts' => $posts
                ]
            ],200);
        }else{
            return response()->json([
                'keyword' => $keyword,
                'message' => 'Không có thông tin về nội dung này!',
            ],200);
        }
    }
}
