<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Account;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Str;

class RegisterController extends Controller
{
    public function register(Request $request)
    {

        $check = User::where('Email', $request -> Email)->first();
        if (isset($check)) {
            return response()->json([
                'status' => 200,
                'message' => 'Tài khoản đã tồn tại !',
            ]);
        }else{
            $user = new User();
            $user->UserName = $request->Email;
            $user->FirstName = $request->FirstName;
            $user->LastName = $request->LastName;
            $user->Birthday = $request->Birthday;
            $user->Gender = $request->Gender;
            $user->ImageUrl = $request->ImageUrl;
            $user -> Introduction = $request->Introduction;
            $user -> Country = $request->Country;
            $user -> Email = $request -> Email;
            $user->PasswordHash = Hash::make($request->Password);
            // $user -> Token = Str::random(32);
            $user->save();
            return response()->json([
              'status' => 200,
              'message' => 'Đăng ký thành công!',

            ]);
        }
    }
}
