<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Auth;
use App\Http\Controllers\Controller;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Tymon\JWTAuth\Facades\JWTAuth;
/**
 * @OA\Post(
 *  path="/api/login",
 *  summary="Đăng nhập ",
 *  description="Đăng nhập ",
 * method="POST",
 * tags={"User"},
 *    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="Email",type="string"),
 *                      @OA\Property(property="PasswordHash",type="string"),
 *                      example={"Email": "trungquanbg3@gmail.com", "Password": "123"}
 *                  )
 *              }
 *         )
 *     ),
*          @OA\Response(
*          response = 200,
*          description = "Success",
*          @OA\JsonContent(
*              @OA\Property(property="user",type="array",@OA\Items(
*                      @OA\Property(property="Id",type="string"),
*                      @OA\Property(property="FirstName",type="string"),
*                      @OA\Property(property="LastName",type="string"),
*                      @OA\Property(property="AvatarId",type="string"),
*                      @OA\Property(property="Introduction",type="string"),
*                      @OA\Property(property="Birthday",type="string"),
*                      @OA\Property(property="Gender",type="string"),
*                      @OA\Property(property="Country",type="string"),
*                      @OA\Property(property="Email",type="string"),
*
*               ),
*           ),
*           @OA\Property(property="token",type="string"),
*       ),
*   ),
* ),
 * @OA\Post(
 *     path="/api/logout",
 *     summary="Đăng xuất ",
 *     description="Đăng xuất ",
 *     tags={"User"},
 *     @OA\Parameter(
 *         name="access_token",
 *         in="header",
 *         description="Điền token JWT",
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
 **/


class AuthController extends Controller
{
    /**
     * Create a new AuthController instance.
     *
     * @return void
     */
    public function __construct()
    {
        $this->middleware('auth:api', ['except' => ['login']]);
    }


    public function login(Request $request)
    {

        $user = User::where('Email',$request -> email) ->first();
        if(!isset($user)){
            return response()->json([
               'success' => false,
               'message' => 'Tài khoản hoặc mật khẩu không chính xác !',
            ]);
        }
        $check = Hash::check($request->password, $user->PasswordHash); // check password

        $user -> save();
        if(isset($user) && $check){
            $token = JWTAuth::fromUser($user);
            $user -> Token = $token;
            return response()->json([
             'success' => true,
             'access_token' => $token,
             'type' => 'Bearer'
            ],200);
        }else{
            return response()->json([
              'success' => false,
              'message' => 'Tài khoản hoặc mật khẩu không chính xác !'
            ],200);
        }
    }
    public function me(){
        $user = auth()->user();
        return response()->json([
            'user' => [
                'FirstName' => $user->FirstName,
                'LastName'  => $user->LastName,
                'ImageUrl'  => $user ->AvatarId,
                'Introduction'=> $user ->Introduction,
                'Birthday'  => $user ->Birthday,
                'Gender'    => $user -> Gender,
                'Country'   => $user -> Country,
                'Email'     => $user -> Email,
             ],
        ],200);
    }

    public function logout()
    {
        auth()->logout();

        return response()->json(['message' => 'Successfully logged out']);
    }


    public function refresh()
    {
        return $this->respondWithToken(auth()->refresh());
    }


    protected function respondWithToken($token)
    {
        return response()->json([
            'access_token' => $token,
            'token_type' => 'bearer',
            'expires_in' => auth()->factory()->getTTL() * 60
        ]);
    }
}
