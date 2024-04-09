<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Mail\ForgotMail;
use App\Models\Account;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Mail;
use Illuminate\Support\Facades\Validator;
use Illuminate\Support\Str;

class ForgotController extends Controller
{
    public function forgot(Request $request)
    {
        // $validate = Validator::make([
        //     'email' => 'required|email',
        // ]);
        $user = User::where('email', $request->email)->first();
            if ($user) {
                $password = Str::random(8);
                $user->Token = Str::random(32);
                $user->PasswordHash = Hash::make($password);
                $user->save();
               try {
                    Mail::to($user->email)->send(new ForgotMail($user, $password));
                    return response()->json([
                        'status' => 'success',
                        'message' => 'Reset password thành công !',
                    ], 200);
               }catch(\Throwable $th){
                    return response()->json([
                        'status' => true,
                        'message' => 'Không thể kết nối được đến gmail !',
                    ], 200);
               }
            } else {
                return response()->json([
                    'message' => 'Email chưa được đăng ký',
                ], 200);
            }
    }
}
