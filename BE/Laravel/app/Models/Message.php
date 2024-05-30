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
        $this -> ConversationId = $request -> conversation_id ?? $request -> id;
        $this -> SenderId = $auth -> Id;
        $this -> RecipientId = $request -> receiver_id;
        $this -> Content = $request -> content;
        $this -> DateSent = date_create();
        $this -> save();
        return true;
    }
}
