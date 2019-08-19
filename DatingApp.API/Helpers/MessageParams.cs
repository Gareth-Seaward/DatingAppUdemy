namespace DatingApp.API.Helpers
{
    public class MessageParams : PagingParams
    {
        public int UserId { get; set; }
        public string MessageContainer { get; set; } = "unread";
    }
}