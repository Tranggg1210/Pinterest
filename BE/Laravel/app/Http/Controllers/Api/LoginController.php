<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Account;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

class LoginController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function login(Request $request)
    {
        $account = Account::where('username',$request -> username) ->first();
        $check = Hash::check($request->password, $account->password); // check password
        if(isset($account) && $check){
            $user = User::where('id',$account -> user_id)->first();
            return response()->json([
             'success' => true,
             'data' => [
                'user' => $user,
                'token' => $account -> token
             ]
            ],200);
        }else{
            return response()->json([
              'success' => false,
              'message' => 'Tài khoản hoặc mật khẩu không chính xác !'
            ],200);
        }
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        //
    }
}
