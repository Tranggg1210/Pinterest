using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
