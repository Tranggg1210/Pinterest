<?php

namespace App\Models;

use Exception;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use App\Models\User;

class Conversation extends Model
{
    protected $table = 'Conversation';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    use HasFactory;
    public function existConversation(User $user,User $connector){
        $conversation = Conversation::where('creatorID',$user->Id)
                            -> where('ConnectorId',$connector -> Id)
                            -> orWhere('CreatorID',$connector->Id)
                            -> where('ConnectorId',$user -> Id)
                            -> first();
            if($conversation){
                return $conversation;
            }
            return false;
    }
    public function createConversation(User $user,User $connector){
            $this->CreatorID = $user -> Id;
            $this->ConnectorId = $connector -> Id;
            $this->CreatedAt = date_create();
            $this->save();
            return $this;
    }
    public function getConversationById(User $user){
        return $this->where('creatorID', $user -> Id)->orWhere('connectorId', $user -> Id)->get();
    }
}
