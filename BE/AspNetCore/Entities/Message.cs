using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int CreatedByUserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual User CreatedByUser { get; set; } = null!;
    }
}
