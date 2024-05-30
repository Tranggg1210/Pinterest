<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Follow extends Model
{
    protected $table = 'Follower';
    protected $fillable = ['FollowerUserId', 'FollowingUserId'];
    public $timestamps = false;
    use HasFactory;

    public function checkFollow(User $Auth,User $followingUser){
        return $this->where('FollowerUserId', $Auth -> Id)->where('FollowingUserId', $followingUser -> Id) -> first();
    }

    public function follow(User $Auth,User $followingUser){
        $follow = new Follow();
        $follow->FollowerUserId = $Auth -> Id;
        $follow->FollowingUserId = $followingUser -> Id;
        // $follow->Status = 1;
        $follow->save();
    }

    public function Unfollow(User $Auth,User $followingUser){
        $this->where('FollowerUserId', $Auth -> Id)->where('FollowingUserId', $followingUser -> Id)->delete();
    }
    public function countFollowers($Id = null){
        return $this->where('FollowingUserId',$Id)->count();
    }
    public function getAllFollowingUsers(User $Auth){
        return $this->where('FollowerUserId', $Auth -> Id)->orderBy('FollowingUserId','desc') -> get('FollowingUserId'); // giảm dần để tối ưu thời gian tìm bài post
    }

}
