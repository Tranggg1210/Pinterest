<?php

use App\Http\Controllers\Api\ForgotController;
use App\Http\Controllers\Api\LoginController;
use App\Http\Controllers\Api\RegisterController;
use App\Http\Controllers\Api\SearchController;
use App\Http\Controllers\AuthController;
use Illuminate\Http\Request;
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

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return auth() -> user();
});
Route::get('/search/{keyword?}',[SearchController::class,'search']) -> name('search');
Route::post('/register',[RegisterController::class,'register']) -> name('register');
Route::post('/forgot',[ForgotController::class,'forgot']) -> name('forgot');

Route::group([

    'middleware' => 'api',

], function ($router) {

    Route::post('login', [AuthController::class,'login']) -> name('login');
    Route::post('logout', [AuthController::class,'logout']) -> name('logout');
    Route::post('refresh', 'AuthController@refresh');
    Route::post('me', 'AuthController@me');

});
