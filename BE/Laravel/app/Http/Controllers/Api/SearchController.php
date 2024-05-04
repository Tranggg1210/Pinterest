<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Post;
use App\Models\User;
use Exception;
use Illuminate\Http\Request;
use Laravel\Scout\Searchable;
/**
 * @OA\Get(
 *     path="/api/search/",
 *     operationId="search",
 *     tags={"Search"},
 *     summary="Tìm kiếm",
 *     description="Tìm kiếm người dùng hoặc bài post",
 *     @OA\Parameter(
 *         name="keyword",
 *         in="query",
 *         required=false,
 *     ),
 *     @OA\Response(
 *         response=200,
 *         description="Success"
 *     )
 * ),
  * @OA\Post(
 *     path="/api/search-user/{keyword}",
 *     summary="Tìm kiếm người dùng",
 *     description="Tìm kiếm người dùng trong phần tin nhắn",
 *     tags={"Search"},
 *     @OA\Parameter(
 *         name="keyword",
 *         in="header",
 *         description="Điền email hoặc tên",
 *         required=true,
 *         @OA\Schema(
 *             type="string"
 *         )
 *     ),
 *     @OA\Response(response=200, description="Logged out successfully"),
 *     @OA\Response(response=400, description="Bad request"),
 *     @OA\Response(response=401, description="Unauthenticated"),
 *     @OA\Response(response=403, description="Forbidden"),
 *     @OA\Response(response=405, description="Method Not Allowed")
 * )
 */


class SearchController extends Controller
{
    public function search(Request $request){
        $keyword = $request -> keyword;
        $users = User::search($keyword)->get()->map(function ($user) {
            return $user->only('Id','FirstName', 'LastName','Email', 'UserName');
        });
        $posts  = Post::search($keyword) -> get();
        return response()->json([
            'status' => 'success',
            'data' => [
                'users' => $users,
                'posts' => $posts,
            ]
        ],200);
    }
    public function shouldBeSearchable()
    {
        return $this->isPublished();
    }
    public function searchUser(Request $request){
        $keyword = $request -> keyword;
        $users = User::search($keyword)->get()->map(function ($user) {
            return $user->only('Id','FirstName', 'LastName','Email');
        });
        return response()->json([
            'status' =>'success',
            'data' => [
                'users' => $users,
            ]
        ],200);
    }
}
