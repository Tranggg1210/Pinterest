<?php

use Illuminate\Support\Facades\Broadcast;
use Tymon\JWTAuth\Facades\JWTAuth;

/*
|--------------------------------------------------------------------------
| Broadcast Channels
|--------------------------------------------------------------------------
|
| Here you may register all of the event broadcasting channels that your
| application supports. The given channel authorization callbacks are
| used to check if an authenticated user can listen to the channel.
|
*/

Broadcast::channel('App.Models.User.{id}', function ($user) {
    return (int) $user->id === (int) $id;
});
Broadcast::channel('follow.{id}', function ($user, $id) {
    try {
        // Xác thực người dùng từ token JWT
        $user = JWTAuth::setToken($user)->authenticate();

        // Kiểm tra xem người dùng có được ủy quyền truy cập vào kênh riêng tư với id tương ứng hay không
        // Ví dụ: Kiểm tra xem người dùng có quyền truy cập vào kênh "follow.1" không
        if ($user && $user->Id == $id) {
            return true;
        } else {
            return false;
        }
    } catch (\Exception $e) {
        // Xử lý lỗi xác thực
        return false;
    }
});
