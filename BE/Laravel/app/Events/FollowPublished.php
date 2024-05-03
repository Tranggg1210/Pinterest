<?php

namespace App\Events;

use App\Models\User;
use BeyondCode\LaravelWebSockets\WebSockets\Channels\PrivateChannel as ChannelsPrivateChannel;
use Illuminate\Broadcasting\Channel;
use Illuminate\Broadcasting\InteractsWithSockets;
use Illuminate\Broadcasting\PresenceChannel;
use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcast;
use Illuminate\Contracts\Broadcasting\ShouldBroadcastNow;
use Illuminate\Foundation\Events\Dispatchable;
use Illuminate\Queue\SerializesModels;

class FollowPublished implements ShouldBroadcastNow
{
    use Dispatchable, InteractsWithSockets, SerializesModels;
    private $notify;
    private User $user;
    /**
     * Create a new event instance.
     */
    public function __construct(User $user,$notify)
    {
        $this ->user = $user;
        $this->notify = $notify;
    }

    /**
     * Get the channels the event should broadcast on.
     *
     * @return array<int, \Illuminate\Broadcasting\Channel>
     */
    public function broadcastOn(): array
    {
        return [
            new PrivateChannel('follow'),
        ];
    }
    public function broadcastWith(){
        return [
            'notify' => $this->notify,
            // 'user' => $this->user,
        ];
    }
}
