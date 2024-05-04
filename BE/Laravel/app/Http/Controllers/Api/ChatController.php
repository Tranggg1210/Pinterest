<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Conversation;
use App\Models\Message;
use App\Models\User;
use Illuminate\Http\Request;

class ChatController extends Controller
{
    public function sendMessage(Request $request,$data = []){
        // $message = new Message();
        // if(count($data))
    }

    public function getConversations(Request $request){
        $content = $request-> content; //Nội dung tin nhắn
        $user = auth() -> user();
        if(!isset($user)){
            return response()-> json(
                [
                   'message' => "Người dùng không tồn tại",
                ],
            );
        }
        $conversations = new Conversation();
        $data = $conversations -> getConversationById($user);
        return response()-> json(
            [
               'message' => "Lấy danh sách cuộc trò chuyện thành công",
                'data' => $data,
            ],
        );
    }
    public function createConversation(Request $request){
        $user = auth() -> user();
        $connector = User::find($request-> id);
        if(!isset($user) || !isset($connector)){
            return response()-> json(
                [
                    'status' => 'nouser',
                   'message' => "Người dùng không tồn tại",
                ],
            );
        }else if($user == $connector){
            return response()-> json(
                [
                    'status'=> 'error',
                   'message' => "Không thể tạo cuộc trò chuyện với chính mình",
                ],
            );
        }
        $check = new Conversation();
        if(!$check =$check->existConversation($user, $connector)){
            $conversation = new Conversation();
            $conversation = $conversation -> createConversation($user, $connector);
            $data = [
                'conversation_id' => $conversation -> Id,
                'content' => $request -> content
            ];
            $this -> sendMessage($request, $data);
            return response()-> json(
                [
                    'status' => 200,
                   'message' => "Tạo cuộc trò chuyện thành công",
                    'data' => [
                        'id_conversation' => $conversation -> Id,
                        'user_id' => $user -> Id,
                        'connector_id' => $connector -> Id,
                    ]
                ],
            );
        }else{
            return response()-> json(
                [
                   'status'=> 'exist',
                   'message' => "Cuộc trò chuyện đã tồn tại",
                    'data' => [
                        'id_conversation' => $check -> Id,
                        'user_id' => $user -> Id,
                        'connector_id' => $connector -> Id,
                    ]
                ],
            );
        }
    }
}
