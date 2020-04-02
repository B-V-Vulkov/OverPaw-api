namespace OverPaw.RequestModels.Account
{
    public class LoginRequestModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsSelectedRememberMe { get; set; }
    }
}
