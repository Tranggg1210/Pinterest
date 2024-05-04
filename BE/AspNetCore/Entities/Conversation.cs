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
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }
        public int ConnectorId { get; set; }

        public virtual User Connector { get; set; } = null!;
        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
