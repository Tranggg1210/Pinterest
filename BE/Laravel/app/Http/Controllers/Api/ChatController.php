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
        $auth = auth()-> user();
        $message = new Message();
        // Nếu có data thì là vừa tạo cuộc hội thoại,không có data thì cuộc hội thoại đã được tạo
        if($data){
            $request->merge(['conversation_id' => $data['conversation_id']]);
            // $request->merge(['senderId' => $data['creator_id']]);
            $request->merge(['user_id' => $data['connector_id']]);
            $request->merge(['content' => $data['content']]);
            // Nếu người gửi và người tạo khác nhau thì fail
            if($auth-> Id != $data['creator_id']){
                return response()-> json([
                   'status' => 502,
                   'message' => "Bạn không có quyền gửi tin nhắn cho người dùng này"
                ]);
            }
            // $message -> conversation_id = $data['conversation_id'];
            // $message -> senderId = $data['creator_id'];
            // $message -> receiverId = $data['connector_id'];
            // $message -> Content = $data['content'];
            // $message -> createdAt = date_create();
            // $message -> save();
            if($message -> createMessage($auth,$request)){
                return response()-> json(
                    [
                       'status' => 200,
                       'message' => "Tạo cuộc trò chuyện thành công",
                        'data' => [
                            'id_conversation' => $data['conversation_id'],
                            'user_id' => $data['creator_id'],
                            'connector_id' => $data['connector_id'],
                            'content' => $data['content'],
                        ]
                    ],
                );
            }
        }else{
            $conversation = Conversation::where('Id',$request->id)-> first();
            // dd($message);
            if(isset($conversation)){
                //Nếu auth là người tạo hội thoại thì lấy người nhận là người kết nối và ngược lại
                $request -> merge(['receiver_id' => $conversation -> connectorId == $auth -> Id ? $conversation -> creatorId : $conversation -> connectorId]);
                if($message -> createMessage($auth,$request)){
                    return response()-> json([
                        'status' => 200,
                        'message' => "Gửi tin nhắn thành công",
                         'data' => [
                             'id_conversation' => $conversation -> Id,
                             'sender_id' => $auth -> Id,
                             'receiver_id' => $conversation -> connectorId == $auth -> Id ? $conversation -> creatorId : $conversation -> connectorId,
                             'content' => $request -> content
                        ]
                     ]);
                }
            }else{
                return response()-> json(
                    [
                       'status' => 404,
                       'message' => "Không tồn tại hội thoại",
                    ],
                );
            }
        }

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
        if(!$check = $check->existConversation($user, $connector)){
            $conversation = new Conversation();
            $conversation = $conversation -> createConversation($user, $connector);
            $data = [
                'conversation_id' => $conversation -> Id,
                'content' => $request -> content,
                'creator_id' => $user -> Id,
                'connector_id' => $connector -> Id,
            ];
            return $this -> sendMessage($request, $data);

        }else{
            // $data = [
            //     'conversation_id' => $check -> Id,
            //     'content' => $request -> content
            // ];
            // $this -> sendMessage($request, $data);
            return response()-> json(
                [
                   'status'=> 'exist',
                   'message' => "Cuộc trò chuyện đã tồn tại",
                    'data' => [
                        'id_conversation' => $check -> Id,
                        'creator_id' => $user -> Id,
                        'connector_id' => $connector -> Id,
                    ]
                ],
            );
        }
    }
}
