using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime DateSent { get; set; }
        public DateTime? DateRead { get; set; }

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual User Recipient { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
