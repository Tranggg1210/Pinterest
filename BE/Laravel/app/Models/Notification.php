<?php

namespace App\Models;

// use Algolia\AlgoliaSearch\Http\Psr7\Request;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Notification extends Model
{
    protected $table = 'Notification';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    use HasFactory;
    public function createNotification(User $user,$data){
        if($user){
            return response()-> json(
                [
                   'message' => "Người dùng không tồn tại",
                ],
            );
        }
        $notification = new Notification();
        $notification->userId = $user -> Id;
        $notification->data = $data;
        $notification->CreatedAt = date_create();
        $notification->save();
        return $notification;
    }
    public function getAllNotifications($userId){
        return $this->where('UserId',$userId)->get();
    }
    // public function deleteNotification($id){
    //     $notification = Notification::find($id);
    //     $notification->delete();
    //     return $notification;
    // }
}
