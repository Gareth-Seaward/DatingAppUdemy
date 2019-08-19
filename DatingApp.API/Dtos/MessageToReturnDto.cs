using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class MessageToReturnDto : MessageCore
    {
        public string SenderKnownAs { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientKnownAs { get; set; }
        public string RecipientPhotoUrl { get; set; }
    }
}