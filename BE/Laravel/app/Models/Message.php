<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Http\Request;

class Message extends Model
{
    protected $table = 'Message';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    use HasFactory;
    public function createMessage(User $auth,Request $request){
        $this -> ConversationId = $request -> id;
        $this -> senderId = $auth -> Id;
        $this -> receiverId = $request -> receiver_id;
        $this -> Content = $request -> content;
        $this -> createdAt = date_create();
        $this -> save();
        return true;
    }
}
