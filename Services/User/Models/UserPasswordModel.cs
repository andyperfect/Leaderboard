namespace Services.User.Models
{
    public class UserPasswordModel : UserModel
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string AccessToken { get; set; }
        public long AccessTokenDate { get; set; }
        
    }
}
