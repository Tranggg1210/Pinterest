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
 *     operationId="searchUsers",
 *     tags={"Search"},
 *     summary="Search users",
 *     @OA\Parameter(
 *         name="keyword",
 *         in="path",
 *         required=false,
 *         @OA\Schema(
 *             type="array",
 *             @OA\Items(
 *                 @OA\Schema(
 *                     @OA\Property(property="Id", type="string"),
 *                     @OA\Property(property="FirstName", type="string"),
 *                     @OA\Property(property="LastName", type="string"),
 *                     @OA\Property(property="ImageUrl", type="string"),
 *                     @OA\Property(property="Introduction", type="string"),
 *                     @OA\Property(property="Birthday", type="string"),
 *                     @OA\Property(property="Gender", type="string"),
 *                     @OA\Property(property="Country", type="string"),
 *                     @OA\Property(property="Email", type="string"),
 *                     example={"Keyword": "trungquanbg3@gmail.com"}
 *                 )
 *             )
 *         )
 *     ),
 *     @OA\Response(
 *         response=200,
 *         description="Success"
 *     )
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

}
