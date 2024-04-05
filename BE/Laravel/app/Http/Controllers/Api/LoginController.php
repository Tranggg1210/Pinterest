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
        $user = User::where('Email',$request -> Email) ->first();
        $check = Hash::check($request->Password, $user->PasswordHash); // check password
        if(isset($user) && $check){
            return response()->json([
             'success' => true,
             'data' => [
                'user' => $user,
                'token' => $user -> token
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
