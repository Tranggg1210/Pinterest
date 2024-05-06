<?php

namespace App\Http\Controllers\Api;

use App\Events\FollowPublished;
use App\Http\Controllers\Controller;
use App\Models\Follow;
use App\Models\Notification;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

use function PHPSTORM_META\type;
/**
 * @OA\Post(
 *     path="/api/follow/{UserId}",
 *     operationId="Follow",
 *     tags={"Follow user"},
 *     summary="Follow",
 *     security={{ "bearerAuth": {} }},
 *     description="Theo dõi người khác khi truyền id",
 *     @OA\Parameter(
 *         name="UserId",
 *         in="path",
 *         required=true,
 *     ),
 *     @OA\Response(response=200,description="Đã follow/bỏ follow"),
 *     @OA\Response(response=400,description="Không thể follow bản thân"),
 *     @OA\Response(response=403,description="Không tồn tại người dùng này"),
 *     @OA\Response(response=404,description="Not found"),
 * ),
 * @OA\Get(
 *     path="/api/get-follower",
 *     operationId="get-follower",
 *     tags={"Follow user"},
 *     summary="Lấy toàn bộ danh sách người đang follow mình",
 *     security={{ "bearerAuth": {} }},
 *     description="Lấy toàn bộ hội thoại của người dùng",
 *     @OA\Response(response=200,description="Thành công"),
 *     @OA\Response(response=404,description="Không tồn tại người dùng"),
 * ),
 */
class FollowController extends Controller
{
    public function follow(Request $request){
        if($request -> UserId == null){
            return response()->json([
               'status' => 404,
               'message' => 'Not found',
            ]);
        }
        $follow = new Follow;
        $FollowerUser = auth() -> user();
        $FollowingUser = User::where('Id',$request -> UserId) -> first();
        if(!$FollowingUser){
            return response()->json([
               'status' => 403,
               'message' => 'Không tồn tại người dùng này',
            ]);
        }
        if($FollowerUser == $FollowingUser){
            return response()->json([
               'status' => 400,
               'message' => 'Không thể follow bản thân',
            ]);
        }
        if($FollowingUser){

            if(is_null($follow -> checkFollow($FollowerUser,$FollowingUser))){
                $follow -> follow($FollowerUser,$FollowingUser);
                $notification = new Notification();
                $data = [
                    'message' =>  $FollowerUser -> FirstName . " đang theo dõi bạn",
                    'FollowingUserId' => $FollowerUser -> Id
                ];
                $notification -> createNotification($FollowingUser,json_encode($data, JSON_UNESCAPED_UNICODE));
                event(new FollowPublished($FollowingUser,[
                    'id_user' => $FollowingUser -> Id,
                    'id_user_followed' => $FollowerUser -> Id,
                    'follow' => true,
                    'status' => 200,
                    'message' => $FollowerUser -> FirstName. " đã follow bạn"
                ]));
                return response()->json([
                    'id_user' => $FollowerUser -> Id,
                    'id_user_following' => $FollowingUser -> Id,
                    'follow' => true,
                    'status' => 200,
                    'message' => "Đã follow"
                ]);
            }else{
                // if(is_null($follow -> checkFollow($FollowerUser,$FollowingUser))){
                //     return response()->json([
                //         'id_user' => $FollowerUser -> Id,
                //         'id_user_following' => $FollowingUser -> Id,
                //         'follow' => false,
                //         'status' => 200,
                //         'message' => 'Chưa follow'
                //     ]);
                // }
                if($follow -> checkFollow($FollowerUser,$FollowingUser)-> Status == "1"){
                    $follow -> Unfollow($FollowerUser, $FollowingUser);
                    return response()->json([
                        'id_user' => $FollowerUser -> Id,
                        'id_user_following' => $FollowingUser -> Id,
                        'follow' => false,
                        'status' => 200,
                        'message' => 'Đã bỏ follow'
                    ]);
                }
            }
        }
    }
    public function getAllFollowers(){
        $user = auth()->user();
        if($user){
            $follow = new Follow();
            $followers = $follow -> getAllFollowers($user);
            $data = [];
            foreach($followers as $follower){
                $user = (new User()) -> getInformationUser($follower['FollowingUserId']);
                array_push($data,$user);
            }
            return response()->json([
               'status' => 200,
                'data' => $data,
            ]);
        }else{
            return response()->json([
               'status' => 404,
               'message' => 'Not found',
            ]);
        }
    }
}
