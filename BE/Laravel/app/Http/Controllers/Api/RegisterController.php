<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Account;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Str;

/**
 * @OA\Post(
 *  path="/api/register",
 * method="POST",
 * summary="Đăng ký ",
 * description="Đăng ký ",
 * tags={"User"},
 *    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="Email",type="string"),
 *                      @OA\Property(property="PasswordHash",type="string"),
*                       @OA\Property(property="Id",type="string"),
*                       @OA\Property(property="FirstName",type="string"),
*                       @OA\Property(property="LastName",type="string"),
*                       @OA\Property(property="AvatarId",type="string"),
*                       @OA\Property(property="Introduction",type="string"),
*                       @OA\Property(property="Birthday",type="string"),
*                       @OA\Property(property="Gender",type="string"),
*                       @OA\Property(property="Country",type="string"),
 *                      example={"Email": "trungquanbg3@gmail.com", "Password": "123","FirstName": "Quân","LastName": "NTQ", "AvatarId": "url", "Introduction": "","Birthday": "09-01-2003","Gender": "1","Country": "VN"}
 *                  )
 *              }
 *         )
 *     ),
*          @OA\Response(
*          response = 200,
*          description = "Success",
*          @OA\JsonContent(
*           @OA\Property(property="status",type="integer"),
*           @OA\Property(property="message",type="string"),
*
*           ),
*       ),
*   ),
* )
 **/
class RegisterController extends Controller
{
    public function register(Request $request)
    {

        $check = User::where('Email', $request -> Email)->first();
        if (isset($check)) {
            return response()->json([
                'status' => 400,
                'message' => 'Tài khoản đã tồn tại !',
            ]);
        }else{
            $user = new User();
            $user->UserName = $request->Email;
            $user->FirstName = $request->FirstName;
            $user->LastName = $request->LastName;
            $user->Birthday = $request->Birthday;
            $user->Gender = $request->Gender;
            $user->AvatarId = $request->AvatarId;
            $user -> Introduction = $request->Introduction;
            $user -> Country = $request->Country;
            $user -> Email = $request -> Email;
            $user->PasswordHash = Hash::make($request->Password);
            $user->save();
            return response()->json([
              'status' => 200,
              'message' => 'Đăng ký thành công!',

            ]);
        }
    }
}
