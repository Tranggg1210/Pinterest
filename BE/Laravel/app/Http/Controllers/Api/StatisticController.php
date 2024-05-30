<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Notification;
use App\Models\Post;
use App\Models\User;
use Illuminate\Http\Request;
/**
 * @OA\Get(
 *     path="/api/count-post",
 *     tags={"Thống kê"},
 *     summary="Số lượng bài post",
 *     @OA\Response(
 *         response=200,
 *         description="Success"
 *     )
 * ),
 * @OA\Get(
 *     path="/api/count-user",
 *     tags={"Thống kê"},
 *     summary="Số lượng người dùng",
 *     @OA\Response(
 *         response=200,
 *         description="Success"
 *     )
 * ),
 * @OA\Get(
 *     path="/api/count-notification",
 *     tags={"Thống kê"},
 *     summary="Số lượng thông báo",
 *     @OA\Response(
 *         response=200,
 *         description="Success"
 *     )
 * ),
 **/
class StatisticController extends Controller
{
    public function countPosts(){
        $post = new Post();
        return response() -> json([
            'count_posts' => $post -> getCountPost()
        ]);
    }
    public function countUsers(){
        $user = new User();
        return response() -> json([
            'count_users' => $user -> getCountPost()
        ]);
    }
    public function countNotifies(){
        $Notification = new Notification();
        return response() -> json([
            'count_notifications' => $Notification -> getCountNotifications()
        ]);
    }

}
