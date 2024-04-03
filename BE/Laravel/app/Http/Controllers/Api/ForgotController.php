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
                $account = Account::where('user_id', $user->id)->first();
                $password = Str::random(8);
                $account->token = Str::random(32);
                $account->password = Hash::make($password);
                $account->save();
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
