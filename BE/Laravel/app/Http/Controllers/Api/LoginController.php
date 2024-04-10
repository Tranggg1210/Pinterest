<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Account;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Validator;
use Illuminate\Support\Str;

/**
 * @OA\Post(
 *  path="/api/login",
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
*                      @OA\Property(property="ImageUrl",type="string"),
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
* )
 **/
class LoginController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function login(Request $request)
    {

        $user = User::where('Email',$request -> Email) ->first();
        if(!isset($user)){
            return response()->json([
               'success' => false,
               'message' => 'Tài khoản hoặc mật khẩu không chính xác !',
            ]);
        }
        $check = Hash::check($request->Password, $user->PasswordHash); // check password
        $token = Str::random(60);
        $user -> Token = $token;
        $user -> save();
        if(isset($user) && $check){
            return response()->json([
             'success' => true,
             'user' => [
                'FirstName' => $user->FirstName,
                'LastName'  => $user->LastName,
                'ImageUrl'  => $user ->ImageUrl,
                'Introduction'=> $user ->Introduction,
                'Birthday'  => $user ->Birthday,
                'Gender'    => $user -> Gender,
                'Country'   => $user -> Country,
                'Email'     => $user -> Email,
             ],
             'token' => $user -> Token
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
