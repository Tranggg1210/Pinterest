<?php

namespace App\Http\Controllers\Api;

use App\Events\ChatPublished;
use App\Http\Controllers\Controller;
use App\Models\Conversation;
use App\Models\Message;
use App\Models\User;
use Illuminate\Http\Request;
/**
 * @OA\Post(
 *     path="/api/create-chat/{id}",
 *     operationId="create-chat",
 *     tags={"Chat"},
 *     summary="Tạo conversation",
 *     description="Tạo conversation khi chat tin đầu tiên.Id của người dùng sẽ được trả về khi tìm kiếm ở phần tìm kiếm trong chat ",
*    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="id",type="integer"),
 *                      @OA\Property(property="content",type="string"),
 *                      example={"id": "Id của người mình đang chat","content": "Nội dung tin nhắn"}
 *                  )
 *              }
 *         )
 *     ),
 *     @OA\Response(response=200,description="Comment successfully"),
 *     @OA\Response(response=202,description="Không thể tạo cuộc trò chuyện"),
 *     @OA\Response(response=404,description="Không tồn tại bài viết"),
 *     @OA\Response(response=414,description="Hội thoại đã tồn tại"),
 * ),
 * @OA\Get(
 *     path="/api/get-conversation",
 *     operationId="get-chat",
 *     tags={"Chat"},
 *     summary="Lấy toàn bộ conversation của người dùng",
 *     description="Lấy toàn bộ hội thoại của người dùng",
 *     @OA\Response(response=200,description="Thành công"),
 *     @OA\Response(response=404,description="Không tồn tại người dùng"),
 * ),
  * @OA\Post(
 *     path="/api/send-message",
 *     operationId="send-message",
 *     tags={"Chat"},
 *     summary="Gửi tin nhắn tới conversation",
 *     description="",
*    @OA\RequestBody(
 *         @OA\JsonContent(
 *             allOf = {
 *                  @OA\Schema(
 *                      @OA\Property(property="id",type="integer"),
 *                      @OA\Property(property="content",type="string"),
 *                      example={"id": "Id của conversation","content": "Nội dung tin nhắn"}
 *                  )
 *              }
 *         )
 *     ),
 *     @OA\Response(response=200,description="Gửi tin nhắn thành công"),
 *     @OA\Response(response=404,description="Không tồn tại hội thoại"),
 * ),
  * @OA\Get(
 *     path="/api/get-message/{id}",
 *     operationId="get-message",
 *     tags={"Chat"},
 *     summary="Lấy toàn bộ tin nhắn của hội thoại",
 *     description="Lấy toàn bộ tin nhắn của hội thoại",
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         description="Điền id conversation",
 *         @OA\Schema(
 *             type="interger"
 *         )
 *     ),
 *     @OA\Response(response=200,description="Thành công"),
 *     @OA\Response(response=404,description="Không tồn tại hội thoại"),
 * ),
 */
class ChatController extends Controller
{
    public function sendMessage(Request $request,$data = []){
        // $auth = auth()-> user();
        $auth = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> get('Id','UserName', 'FirstName', 'LastName', 'Email', 'Country');
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
                $data = [
                    'id_conversation' => $data['conversation_id'],
                    'sender_id' => $data['creator_id'],
                    'receiver_id' => $data['connector_id'],
                    'content' => $data['content'],
                ];
                event(new ChatPublished($data));
                return response()-> json(
                    [
                       'status' => 200,
                       'message' => "Tạo cuộc trò chuyện thành công",
                        'data' => $data
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
                    $data= [
                        'id_conversation' => $conversation -> Id,
                        'sender_id' => $auth -> Id,
                        'receiver_id' => $conversation -> connectorId == $auth -> Id ? $conversation -> creatorId : $conversation -> connectorId,
                        'content' => $request -> content
                    ];
                    event(new ChatPublished($data));
                    return response()-> json([
                        'status' => 200,
                        'message' => "Gửi tin nhắn thành công",
                         'data' => $data
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
        // $content = $request-> content; //Nội dung tin nhắn
        // $user = auth() -> user();
        $user = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> get('Id','UserName', 'FirstName', 'LastName', 'Email', 'Country');
        if(!isset($user)){
            return response()-> json(
                [
                    'status' => 404,
                   'message' => "Người dùng không tồn tại",
                ]
            );
        }
        $conversations = new Conversation();
        $data = $conversations -> getConversationById($user);
        return response()-> json(
            [
                'status' => 200,
               'message' => "Lấy danh sách cuộc trò chuyện thành công",
                'data' => $data,
            ]
        );
    }
    public function createConversation(Request $request){
        // $user = auth() -> user();
        $user = User::where('Token',explode(' ',$request->header('Authorization'))[1]) -> get('Id','UserName', 'FirstName', 'LastName', 'Email', 'Country');
        $connector = User::find($request-> id);
        if(!isset($user) || !isset($connector)){
            return response()-> json(
                [
                    'status' => 404,
                   'message' => "Người dùng không tồn tại",
                ],
            );
        }else if($user == $connector){
            return response()-> json(
                [
                    'status'=> 202,
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
                   'status'=> 414,
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
    public function getAllMessage(Request $request){
        $conversation_id = $request-> id;
        $conversation = Message::where('ConversationId', $conversation_id)-> get();
        if($conversation){
            return response()-> json([
                'status'=> 200,
                'data' => $conversation
            ]);
        }else{
            return response()-> json([
               'status'=> 404,
               'message' => "Không tồn tại hội thoại",
            ]);
        }
    }
}
