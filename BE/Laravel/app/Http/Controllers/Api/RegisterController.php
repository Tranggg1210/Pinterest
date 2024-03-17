<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Account;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Str;

class RegisterController extends Controller
{
    public function register(Request $request)
    {
        // $this->validate($request, [
        //     'username' =>'required|unique',
        //     'password' =>'required',
        //     'confirm_password' =>'required|same:password',
        // ]);
        $account = new Account();
        // $check = DB::table('account')
        //         ->where('username', $request->username)
        //         ->where('password', $request->password)
        //         ->first();
        $check = Account::where('username', $request -> username)->first();
        if (isset($check)) {
            return response()->json([
             'status' => 200,
             'message' => 'Tài khoản đã tồn tại !',
            ]);
        }else{
            $account->username = $request->username;
            $account->password = Hash::make($request->password);
            $account -> token = Str::random(32);
            $account->save();
            return response()->json([
              'status' => 200,
              'message' => 'Đăng ký thành công!',
              'data' => [
                'username' => $account->username
              ]
            ]);
        }
    }
}
