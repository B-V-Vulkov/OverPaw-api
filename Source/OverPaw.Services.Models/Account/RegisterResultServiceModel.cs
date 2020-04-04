namespace OverPaw.Services.Models.Account
{
    public class RegisterResultServiceModel
    {
        public int Status { get; set; }

        public bool IsEmailTaken { get; set; }

        public bool IsUsenameTaken { get; set; }
    }
}
