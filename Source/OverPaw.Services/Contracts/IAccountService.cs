namespace OverPaw.Services.Contracts
{
    using System.Threading.Tasks;

    using RequestModels.Account;
    using Services.Models.Account;

    public interface IAccountService
    {
        Task<RegisterResultServiceModel> RegisterUserAsync(RegisterRequestModel requestModel);

        Task<LoginResultServiceModel> LoginUserAsync(LoginRequestModel requestModel);
    }
}
