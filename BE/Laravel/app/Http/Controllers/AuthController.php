<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Auth;
use App\Http\Controllers\Controller;
use App\Models\User;
use Exception;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Tymon\JWTAuth\Facades\JWTAuth;


class AuthController extends Controller
{
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

        if(isset($user) && $check){
            $token = JWTAuth::fromUser($user);
            $user -> Token = $token;
            $user -> save();
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
        // $user = auth()->user();
        $user = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> get('Id','UserName', 'FirstName', 'LastName', 'Email', 'Country');
        if($user){
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
        }else{
            return response()->json([
               'success' => false,
               'message' => 'Bạn chưa đăng nhập'
            ],200);
        }
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
