namespace Services.User.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public long DateCreated { get; set; }
    }
}
