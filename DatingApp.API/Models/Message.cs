using System;

namespace DatingApp.API.Models
{
    public class Message : MessageCore
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}