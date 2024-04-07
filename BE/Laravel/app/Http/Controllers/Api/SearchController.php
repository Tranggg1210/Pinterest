<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Post;
use App\Models\User;
use Exception;
use Illuminate\Http\Request;
use Laravel\Scout\Searchable;
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
