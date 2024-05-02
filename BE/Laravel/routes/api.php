<?php

use App\Http\Controllers\Api\CommentController;
use App\Http\Controllers\Api\FollowController;
use App\Http\Controllers\Api\ForgotController;
use App\Http\Controllers\Api\LoginController;
use App\Http\Controllers\Api\NotificationController;
use App\Http\Controllers\Api\RegisterController;
use App\Http\Controllers\Api\SearchController;
use Illuminate\Http\Request;
use App\Http\Controllers\AuthController;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::get('/search/{keyword?}',[SearchController::class,'search']) -> name('search');
Route::post('/register',[RegisterController::class,'register']) -> name('register');
Route::post('/forgot',[ForgotController::class,'forgot']) -> name('forgot');

Route::group(['middleware' => 'api',], function ($router) {
    Route::post('login', [AuthController::class,'login']) -> name('login');
});
// Route::middleware('auth:api')->get('/user', function (Request $request) {
    //     return auth() -> user();
    // });
    Route::middleware("auth:api")->group(function(){
    Route::get('me', [AuthController::class,'me']) -> name('me');
    Route::post('logout', [AuthController::class,'logout']) -> name('logout');
    Route::post('follow/{UserId?}', [FollowController::class,'follow']) -> name('follow');;
    // Route::post('notifications', [NotificationController::class,'notification']) -> name('notification');;
    Route::post('comments/{PostId?}', [CommentController::class,'sendcomment']) -> name('comment');
});
Route::get('get-comment/{PostId?}', [CommentController::class,'getComments']) -> name('getComment');
