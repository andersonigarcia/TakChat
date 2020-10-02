namespace TakChat.Data
{
    public class UserDetail
    {
        public UserDetail(string connectionId, string userName)
        {
            ConnectionId = connectionId;
            UserName = userName;
        }

        public string ConnectionId { get; set; }
        public string  UserName { get; set; }
    }
}
