<?php

namespace App\Models;

use Exception;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Conversation extends Model
{
    protected $table = 'Conversation';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    use HasFactory;
    public function existConversation(User $user,User $connector){
        $conversation = Conversation::where('creatorID',$user->Id)
                            -> where('connectorId',$connector -> Id)
                            -> orWhere('creatorID',$connector->Id)
                            -> where('connectorId',$user -> Id)
                            -> first();
            if($conversation){
                return $conversation;
            }
            return false;
    }
    public function createConversation(User $user,User $connector){
            $this->creatorID = $user -> Id;
            $this->connectorId = $connector -> Id;
            $this-> createdAt = date_create();
            $this->save();
            return $this;
    }
    public function getConversationById(User $user){
        return $this->where('creatorID', $user -> Id)->orWhere('connectorId', $user -> Id)->get();
    }
}
